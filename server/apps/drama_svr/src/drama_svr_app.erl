%%%-------------------------------------------------------------------
%% @doc drama_svr public API
%% @end
%%%-------------------------------------------------------------------

-module(drama_svr_app).

-behaviour(application).

%% Application callbacks
-export([start/2, stop/1]).

-include("com_def.hrl").

%%====================================================================
%% API
%%====================================================================

start(_StartType, _StartArgs) ->
  {ok, DramaPort}  = config:get(drama_svr, drama_port),
  Dispatch = cowboy_router:compile([
    {'_', [
      {"/", drama_svr, []}
    ]}
  ]),
  {ok, _Pid} = cowboy:start_clear(http, [{port, DramaPort}], #{
    env => #{dispatch => Dispatch}
  }),
  ?INFO("drama svr ws started"),
  drama_svr_sup:start_link().

%%--------------------------------------------------------------------
stop(_State) ->
    ok.

%%====================================================================
%% Internal functions
%%====================================================================
