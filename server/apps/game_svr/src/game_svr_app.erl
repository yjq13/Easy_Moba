%%%-------------------------------------------------------------------
%% @doc game_svr public API
%% @end
%%%-------------------------------------------------------------------

-module(game_svr_app).

-behaviour(application).

-include("com_def.hrl").

%% Application callbacks
-export([start/2, stop/1]).

%%====================================================================
%% API
%%====================================================================

start(_StartType, _StartArgs) ->
  {ok, Pid} = game_svr_sup:start_link(),
  ?INFO("[GameSvr] start successfully, cheers!"),
  {ok, Pid}.

%%--------------------------------------------------------------------
stop(_State) ->
  ?INFO("[GameSvr] stopped."),
	ok.

%%====================================================================
%% Internal functions
%%====================================================================
