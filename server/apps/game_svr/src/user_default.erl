-module(user_default).
-compile([debug_info]).

-include("ets.hrl").
-include("record.hrl").
-include("com_def.hrl").
-include("share.hrl").

-define(COMPILE_OPTS, [
  {i, "libs/csv_lib/include/"},
  {i, "libs/share/include/"},
  {i, "libs/proto/include/"},
  {i, "apps/game_svr/include/"},
  {parse_transform, lager_transform},
  export_all]).

-dialyzer({[no_missing_calls], [c/0, cover/1, cover_dump/1]}).

-export([c/0, info_log/0, debug_log/0, error_log/0, cover/1, cover_dump/1]).

c() ->
  erlang:apply(r3, do, [compile]).

info_log() ->
  lager:set_loglevel(lager_console_backend, info).

debug_log() ->
  lager:set_loglevel(lager_console_backend, debug).

error_log() ->
  lager:set_loglevel(lager_console_backend, error).

cover(Src) ->
  cover:start(),
  cover:compile(Src, ?COMPILE_OPTS).

cover_dump(Mod) ->
  cover:analyse_to_file(Mod),
  cover:stop().
