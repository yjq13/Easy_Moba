ENV=dev
ENV_CFG_PATH=config/env/${ENV}/env.ini
source ${ENV_CFG_PATH}

SERVER=$1
if [ -z $1 ]; then
	SERVER=$GAME_SVR
fi

echo "start ${SERVER}"
rebar3 shell --sname ${SERVER} --setcookie $COOKIE --apps ${SERVER} --config config/_output/${SERVER}.config