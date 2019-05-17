#!/bin/bash
#一个结合rebar3与实际项目的模块
if [ $# -lt 1 ]
then
    echo "usage $0 [shell]"
    exit
fi
ALL_APP="game_svr"
ALL_APP_IN_STOP="game_svr"

OP=$1
SERVER=$2
CMD=$3
VERSION=0.1.0
CLI_VERSION=1.1.0
BRANCH_NO=`basename $(dirname "$PWD")`

PROD_DIR=_build/prod/rel
LIB_DIR=_build/prod/lib

SVR_NODE_INI_FILE=config/env/dev/node.ini
WATCH_DOG="-heart -env ERL_CRASH_DUMP_SECONDS 30"

P4_HOME=~/

#default variables
set_default_vars() {
    GAME_MODE=prod
    BT_MODE=prod
    ENV=prod
}

#path
set_cfg_path(){
    EASY_MOBA_CFG=~/.easy_moba.ini
    SYS_CFG_TMPL=config/sys.config.tmpl
    VM_ARGS_TMPL=config/vm.args.tmpl
    OUTPUT_CONFIG_FILE=config/_output/$SERVER.config
    OUTPUT_VM_FILE=config/_output/${SERVER}_vm.args
}

#source files
invoke_file() {
    if [[ -a $EASY_MOBA_CFG ]]; then
      source $EASY_MOBA_CFG
    fi
    ENV_DIR=config/env/$ENV
    source $ENV_DIR/env.ini
    ## 不要删除，这边是为了再替换一次
    if [[ -a $EASY_MOBA_CFG ]]; then
      source $EASY_MOBA_CFG
    fi
}

init_env() {
  set_default_vars
  set_cfg_path
}

load_host() {
  #linux_user,linux_host,svr_type,svr_id,work_id,port
  while IFS="," read f1 f2 f3 f4 f5 f6 f7 || [[ -n ${f1} ]];
  do
      # If the line starts with ST then echo the line
      # if [[ $f1 = $USER ]] && [[ $f2 = $HOSTNAME ]] && [[ $f3 = $SERVER ]]; then
      if [[ $f1 = $USER ]] && [[ $f3 = $SERVER ]]; then
          export SERVER_ID=$f4
          export WORKER_ID=$f5
          export SERVER_IP=$f6
          export SERVER_PORT=$f7
      fi
  done < "$ENV_DIR/host.ini"
}


function list_include_item {
  local list="$1"
  local item="$2"
  if [[ $list =~ (^|[[:space:]])"$item"($|[[:space:]]) ]] ; then
    # yes, list include item
    result=0
  else
    result=1
  fi
  return $result
}

gen_cfg() {
  source $SYS_CFG_TMPL
  eval Cfgs='$'{"$SERVER"_cfgs}
  Cfgs=${Cfgs//\$SERVER/$SERVER}
  Cfgs=${Cfgs//\$SERVER_ID/$SERVER_ID}
  echo -e "$Cfgs"   > $OUTPUT_CONFIG_FILE
  SED_VMARGS=" s/\${SERVER_ID}/${SERVER_ID}/g;"
  SED_VMARGS+=" s/\${SERVER_IP}/${SERVER_IP}/g;"
  SED_VMARGS+=" s/\${SERVER}/${SERVER}/g;"
  SED_VMARGS+=" s/\${COOKIE}/${COOKIE}/g;"
  sed -e "${SED_VMARGS}" $VM_ARGS_TMPL > $OUTPUT_VM_FILE
}

prepare(){
  init_env
  invoke_file
  load_host
  gen_cfg
  }

shell() {
   prepare
   rebar3 shell --name ${SERVER}_${SERVER_ID}@${SERVER_IP} --setcookie $COOKIE --apps ${SERVER} --config config/_output/${SERVER}.config
}

cmd() {
  ${PROD_DIR}/${SERVER}/bin/$SERVER $1
}

ping() {
  for i in $ALL_APP
  do
    echo "[$(${PROD_DIR}/$i/bin/$i ping)] $i"
  done
}

start_svr() {
  if [[ $SERVER == game_svr ]] && [[ $2 == time_adjustable ]]; then
    echo -e "start $SERVER \c"
    echo "$(${PROD_DIR}/$SERVER/bin/$SERVER start +c false)"
  else
    echo -e "start $SERVER \c"
    echo "$(${PROD_DIR}/$SERVER/bin/$SERVER start)"
  fi
}

stop_svr() {
  echo -e "stop $SERVER: \c"
  echo "$(${PROD_DIR}/$SERVER/bin/$SERVER stop)"
}

build_all(){
  build_db
  }

add_hrl(){
  find . -type d \( -path ./_build -o -path ./libs/csv_lib -o -path ./3rd \) -prune -o -name "*.hrl" | awk -F"/" '{print "-include(\""$NF"\")."}' | grep hrl > tmp
  echo "-include(\"csv.hrl\")." >> tmp
}

compile_and_load_file() {
  file_path=$1
  ebin_path=$2
  mode=$3
  include="-I $PROD_DIR/$SERVER/lib/csv_lib-${VERSION}/include/ \
    -I $PROD_DIR/$SERVER/lib/share-${VERSION}/include/ \
    -I $PROD_DIR/$SERVER/lib/proto-${VERSION}/include/ \
    -I $PROD_DIR/$SERVER/lib/redis-${VERSION}/include/ \
    -I $PROD_DIR/$SERVER/lib/$SERVER-${VERSION}/include/"
  mod_name=`basename $file_path | cut -d '.' -f 1`

  exec_file=./$PROD_DIR/$SERVER/bin/$SERVER
  lager_ebin=$PROD_DIR/$SERVER/lib/lager-3.6.3/ebin/
  erlc -o $ebin_path -W $include  -pa $lager_ebin +'{parse_transform, lager_transform}' +'{lager_truncation_size, 1024}' $file_path 

  if [[ $mode == soft ]]; then
    $exec_file eval "code:soft_purge(${mod_name})"
  else
    $exec_file eval "code:purge(${mod_name})"
  fi
    $exec_file eval "code:load_file(${mod_name})"
}

load_beam() {
  mod_name=$1
  mode=$2
  exec_file=./$PROD_DIR/$SERVER/bin/$SERVER
  if [[ $mode == soft ]]; then
    $exec_file eval "code:soft_purge(${mod_name})"
  else
    $exec_file eval "code:purge(${mod_name})"
  fi
    $exec_file eval "code:load_file(${mod_name})"
}

clean() {
  rm -rf _build
  rm -f config/_output/*
  rm -f 3rd/*/ebin/*
  rm -f libs/proto/src/_erl/*
  echo "_build/... cleaned!"
}

release() {
    prepare
    rm ${PROD_DIR}/$SERVER -rf
    echo "rebar3 as prod release -n $SERVER"
    rebar3 as prod release -n $SERVER
}

batch() {
    if [ -z $2 ] || [ $2 == all ]; then
      if [[ $1 == stop_svr ]]; then
        APPS=$ALL_APP_IN_STOP
      else
        APPS=$ALL_APP
      fi
      for APP in $APPS
        do
          SERVER=$APP
          $1 $APP $3
        done
    else
      $1 $APP $3
    fi
}

## 杀掉相关进程
kill() {
  process_ids=`ps -ef |grep $USER |grep erl|grep -v grep|grep -v jenkins | awk '{print $2}'`
  if [[ -n $process_ids ]]; then
    echo $process_ids | xargs -n 1 kill -9
    echo "kill process done"
  else
    echo "no process to kill, pass"
  fi
}

# ========================================================================
case $OP in
  shell )
    shell
    ;;
  start )
    # 时间可调: ./dev.sh start all time_adjustable
    batch start_svr $2 $3
    sleep 5
    ping
    ;;
  stop )
   batch stop_svr $2 $3
    ;;
  a|attach )
    cmd attach
    ;;
  rc|remote_console )
    cmd remote_console
    ;;
  f|foreground )
    cmd foreground
    ;;
  c|console )
    cmd console
    ;;
  include )
    add_hrl
    ;;
  clean )
    clean
    ;;
  rel )
    batch release $2 $3
    ;;
  ping )
    ping
    ;;
  kill )
    kill
    ;;
  * )
    echo "Unknow Command."
esac
