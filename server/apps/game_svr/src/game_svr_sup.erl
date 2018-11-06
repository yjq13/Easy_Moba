%%%-------------------------------------------------------------------
%% @doc game_svr top level supervisor.
%% @end
%%%-------------------------------------------------------------------

-module(game_svr_sup).

-behaviour(supervisor).

-include("com_def.hrl").

%% API
-export([start_link/0]).

%% Supervisor callbacks
-export([init/1]).

-define(SERVER, ?MODULE).

%%====================================================================
%% API functions
%%====================================================================

start_link() ->
    supervisor:start_link({local, ?SERVER}, ?MODULE, []).

%%====================================================================
%% Supervisor callbacks
%%====================================================================

%% Child :: #{id => Id, start => {M, F, A}}
%% Optional keys are restart, shutdown, type, modules.
%% Before OTP 18 tuples must be used to specify a child. e.g.
%% Child :: {Id,StartFunc,Restart,Shutdown,Type,Modules}
init([]) ->
  ChildSpecs = child_specs(),
  {ok, {{one_for_one, 10, 30}, ChildSpecs}}. % 10s内重启子进程30次

%%====================================================================
%% Internal functions
%%====================================================================

child_specs() ->
  % Child不可以重启的监控树
  SupUnRst  = ?CHILD(sup_unrst, worker),
  SupRst    = ?CHILD(sup_rst, worker),
  SupAfter  = ?CHILD(sup_after, worker),
  SupWs     = ?CHILD(sup_ws, worker),
  [SupUnRst, SupRst, SupAfter, SupWs].