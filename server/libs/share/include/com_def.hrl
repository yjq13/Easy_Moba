-ifndef(LIB_H).
-define(LIB_H, lib_h).

%% LIB Settings
%% lager output
-define(DEBUG(Fmt, Args),    catch lager:debug(Fmt, Args)).
-define(INFO(Fmt, Args),     catch lager:info(Fmt, Args)).
-define(WARNING(Fmt, Args),  catch lager:warning(Fmt, Args)).
-define(ERROR(Fmt, Args),    catch lager:error(Fmt, Args)).

-define(DEBUG(Arg),          catch lager:debug("~p",   [Arg])).
-define(INFO(Arg),           catch lager:info("~p",    [Arg])).
-define(WARNING(Arg),        catch lager:warning("~p", [Arg])).
-define(ERROR(Arg),          catch lager:error("~p",   [Arg])).

%% supervisor child
-define(CHILD(I), {I, {I, start_link, []}, permanent, 5000, worker, [I]}).
-define(CHILD(I, Type), {I, {I, start_link, []}, permanent, 5000, Type, [I]}).
-define(CHILD(I, Args, Type), {I, {I, start_link, Args}, permanent, 5000, Type, [I]}).
-define(CHILD(N, I, Args, Type), {N, {I, start_link, Args}, permanent, 5000, Type, [I]}).

%% pr
-define(PR(Content), lager:pr(Content, ?MODULE, [compress])).
-define(PR_ST(Stacktrace, Content), lager:pr_stacktrace(Stacktrace, Content)).

%% if
-define(IF(BOOL, V1, V2), tools:if_then(BOOL, V1, V2)).
-define(IF_UDEF(Val, V1, V2), tools:if_then(Val =:= undefined, V1, V2)).

%% time
-define(SPAN_POINT,     0).  							%%% 每天0点刷新
-define(ONE_MINUTE_SEC, 60).
-define(ONE_HOUR_SEC,   (60	*	60)).
-define(ONE_DAY_SEC,    (24 * 60 * 60)).

%% hot key
-define(NOW,                 time_util:now()).

%% settings
-define(HEARTBEAT_DELAY_TIME, 5).

-endif.