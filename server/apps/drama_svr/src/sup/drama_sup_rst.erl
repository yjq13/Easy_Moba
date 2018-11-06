%%%-------------------------------------------------------------------
%% @doc drama_svr top level supervisor.
%% @end
%%%-------------------------------------------------------------------

-module(drama_sup_rst).

-behaviour(supervisor).

-include("com_def.hrl").

%% API
-export([start_link/0]).

%% Supervisor callbacks
-export([init/1]).

%%====================================================================
%% API functions
%%====================================================================

start_link() ->
    supervisor:start_link({local, ?MODULE}, ?MODULE, []).

%%====================================================================
%% Supervisor callbacks
%%====================================================================

%% Child :: {Id,StartFunc,Restart,Shutdown,Type,Modules}
init([]) ->
  ChildSpecs = child_specs(),
  {ok, {{one_for_one, 10, 30}, ChildSpecs}}. % 10s内重启子进程30次

%%====================================================================
%% Internal functions
%%====================================================================
child_specs() -> 
  [].