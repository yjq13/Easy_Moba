-module(config).

-include("err_code.hrl").
-include("com_def.hrl").

-export([get_val/3, get/2, get/3]).

get(App, Type) ->
  case  application:get_env(App, Type) of
    {ok, Val}  ->
      {ok, Val};
    _->
      ?ERROR("~p get/2 not found ~p ~p ", [?MODULE, App, Type]),
      ?ERR_ENV_CONFIG_NOT_FOUND
  end.

get(App, Type, nowarn) ->
  case  application:get_env(App, Type) of
    {ok, Val}  ->
      {ok, Val};
    _->
      ?ERR_ENV_CONFIG_NOT_FOUND
  end;
get(App, Type, DefVal) ->
  case application:get_env(App, Type) of
    {ok, Val}  ->
      {ok, Val};
    _->
      DefVal
  end.

get_val(App, Env, Type) ->
  case application:get_env(App, Env) of
    {ok, List} ->
      case proplists:get_value(Type, List) of
        undefined ->
          ?ERROR("~p get_val/3 undefined not found ~p ~p ~p", [?MODULE, App, Env, Type]),
          ?ERR_ENV_CONFIG_NOT_FOUND;
        Val ->
          {ok, Val}
      end;
    undefined ->
      ?ERROR("~p get_val/3 undefined not found ~p ~p ~p", [?MODULE, App, Env, Type]),
      ?ERR_ENV_CONFIG_NOT_FOUND
  end.