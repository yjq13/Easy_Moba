#!/bin/sh

proto_lib=libs/proto/msg
csv_lib=libs/csv_lib

sync_proto()
{
  ##拉取协议，并且建立硬连接
  ln -f ../proto/* $proto_lib
  echo "hard link client proto, ok."
}

sync_csv()
{
  rm $csv_lib/csv -rf
  # 拷贝cs到指定位置
  cp -rf ../csv/ $csv_lib
}

csv2record()
{
  # 转换csv文件到erlang record格式
  csv2record=3rd/csv2record
  cd $csv2record
  rebar3 escriptize
  cd -
  csv2record_exe=${csv2record}/_build/default/bin/csv2record
  csv_opt="ebin_dir=${csv_lib}/ebin;hrl_dir=${csv_lib}/include;src_dir=${csv_lib}/src;"
  rm -f ${csv_lib}/src/*.erl
  rm -f ${csv_lib}/include/*
  $csv2record_exe ${csv_lib}/csv $csv_opt && cat ${csv_lib}/include/csv_*.hrl > ${csv_lib}/include/csv.hrl
  rm -rf  ${csv_lib}/ebin
 }

help()
{
    echo ""
    echo "Default to sync proto, sync csv, transform csv to record."
    echo ""
    echo "Optional parameters:"
    echo "  proto       only sync proto file."
    echo "  csv         only sync csv file."
    echo "  record      only transform csv to record."
    echo "  help        print help info."
    echo ""
}

case $1 in
  proto) sync_proto;;
  csv) sync_csv;;
  record) csv2record;;
  help) help;;
  *)
    sync_proto
    sync_csv
    csv2record
esac
