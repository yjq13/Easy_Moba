-module(time_util).

-include("com_def.hrl").

-define(DEFAULT_DAY_HOUR, 0).  %% 每天的结算时间，默认早上5点

-export([now/0
      , now_ms/0
      , now_us/0
      , datetime/0
      , local_time/0
      , universal_time/0
      , timestamp_to_datetime/1
      , timestamp_to_utc_dt/1
      , time_to_local_dt_str/1
      , time_to_utc_dt_str/1
      , time_to_string/1
      , string_to_ts/1
      , is_same_hour/1
      , is_same_hour/2
      , is_same_day/1
      , is_same_day/2
      , is_same_day_zero/1
      , the_same_month/1
      , get_diff_days/2
      , is_same_week/1
      , is_less/2
      , today_tuple/0
      , today_tuple/1
      , calc_datetime/1
      , local_time2seconds/1
      , local_today_seconds/0
      , yesterday_week_day/1
      , nth_days_after/1
      , nth_days_after/2
      , nth_days_before/1
      , nth_days_before/2
      , nth_hours_after/1
      , nth_hours_after/2
      , nth_hours_before/1
      , nth_hours_before/2
      , span_point/2
      , sleep/1
      , is_point/1
      , in_period/2
      , date/0
      , date_before/1
      , time_zone/0
      ]).

%% ------------------------------------------------------------------
%% Time Function Exports
%% ------------------------------------------------------------------
% 时间戳 number since 1970/1/1 0:0:0
now() ->
  {A, B, _} = erlang:timestamp(),
  A * 1000 * 1000 + B.

%% 10e-3 second
now_ms() ->
  {A, B, C} = erlang:timestamp(),
  (A * 1000 * 1000 + B) * 1000 + (C div 1000).

now_us() ->
  {A, B, C} = erlang:timestamp(),
  (A * 1000 * 1000 + B) * 1000 * 1000 + C.

local_time() ->
  erlang:localtime().

universal_time() ->
  erlang:universaltime().

%% @spec "2015-1-29 1:24:36"
datetime() ->
  {{Y, M, D}, {H, MM, S}} = time_util:local_time(),
  lists:concat([Y, '-', M, '-', D, ' ', H, ':', MM, ':', S]).

%% 每个字段都是2个宽度
time_to_string({{Y, M, D}, {H, MM, S}}) ->
  lists:flatten(io_lib:format("~B-~2.10.0B-~2.10.0B ~2.10.0B:~2.10.0B:~2.10.0B"
    , [Y, M, D, H, MM, S])).

%% 时间戳转本地时间
%% @ret {{Y, M, D}, {H, MM, S}}
timestamp_to_datetime(TimeStamp) ->
  MegaS = TimeStamp div (1000*1000),
  S = TimeStamp rem (1000*1000),
  calendar:now_to_local_time({MegaS, S, 0}).

%% @ret "2015-01-29 01:24:36"
time_to_local_dt_str(TimeStamp) ->
  time_to_string(timestamp_to_datetime(TimeStamp)).

% 时间戳转utc时间
% {{Y, M, D}, {H, MM, S}}
timestamp_to_utc_dt(TimeStamp) ->
  MegaS = TimeStamp div (1000*1000),
  S = TimeStamp rem (1000*1000),
  calendar:now_to_datetime({MegaS, S, 0}).

%% @ret "2015/01/29 01:24:36"
time_to_utc_dt_str(TimeStamp) ->
  time_to_string(timestamp_to_utc_dt(TimeStamp)).

string_to_ts([]) -> 0;
string_to_ts(DateTimeStr) ->
  [Y, M, D, H, Min, S] = string:tokens(DateTimeStr, ": /"),
  F = fun list_to_integer/1,
  local_time2seconds({{F(Y), F(M), F(D)}, {F(H), F(Min), F(S)}}).

% 检查日期是否是同一个月
the_same_month(0) -> false;
the_same_month(TimeStamp) ->
  TimeStamp_Offset = TimeStamp - 3600 * 5,
  Timenow_Offset = time_util:now() - 3600 * 5,
  {{Y1, M1, _}, _} = timestamp_to_datetime(TimeStamp_Offset),
  {{Y2, M2, _}, _} = timestamp_to_datetime(Timenow_Offset),
  {Y1, M1} == {Y2, M2}.

% 检查日期是否是同一天, 按标准0点计算
is_same_day_zero(0) -> false;
is_same_day_zero(TimeStamp) ->
  is_same_day(TimeStamp, time_util:now(), 0).

% 检查日期是否是同一天, 按早上5点算一天的起点
is_same_day(TimeStamp) ->
  is_same_day(TimeStamp, time_util:now()).
is_same_day(T1, T2) when (T1 =:= 0) or (T2 =:= 0) -> false;
is_same_day(T1, T2) ->
  is_same_day(T1, T2, ?DEFAULT_DAY_HOUR).

is_same_day(T1, T2, _HourOffset) when (T1 =:= 0) or (T2 =:= 0) -> false;
is_same_day(T1, T2, HourOffset) ->
  TimeStamp_Offset = T1 - 3600 * HourOffset,
  Timenow_Offset = T2 - 3600 * HourOffset,
  {{Y1, M1, D1}, _} = timestamp_to_datetime(TimeStamp_Offset),
  {{Y2, M2, D2}, _} = timestamp_to_datetime(Timenow_Offset),
  {Y1, M1, D1} == {Y2, M2, D2}.

%% 从检查日期是否同一周
is_same_week(0) -> false;
is_same_week(TimeStamp) ->
  TimeStamp_Offset = TimeStamp - 3600 * ?DEFAULT_DAY_HOUR + 86400,
  Timenow_Offset = time_util:now() - 3600 * ?DEFAULT_DAY_HOUR + 86400,
  {{Y1, M1, D1}, _} = timestamp_to_datetime(TimeStamp_Offset),
  {{Y2, M2, D2}, _} = timestamp_to_datetime(Timenow_Offset),
  Week1 = calendar:iso_week_number({Y1, M1, D1}),
  Week2 = calendar:iso_week_number({Y2, M2, D2}),
  Week1 == Week2.

is_same_hour(T1) ->
  is_same_hour(T1, ?NOW).
is_same_hour(T1, T2) ->
  {_, {H1, _, _}} = timestamp_to_datetime(T1),
  {_, {H2, _, _}} = timestamp_to_datetime(T2),
  H1 == H2.

calc_datetime(Time) ->
  calendar:now_to_local_time({Time div (1000*1000), Time rem (1000*1000), 0}).

%% 今天已经过去的秒数
local_today_seconds() ->
  {_, {H, M, S}} = time_util:local_time(),
  H * 60 * 60 + M * 60 + S.

%% {Year, Month, Day}
yesterday_week_day({Y, M, D}) ->
  Week = calendar:day_of_the_week({Y, M, D}),
  yesterday_week_day(Week);
yesterday_week_day(7) -> 6;
yesterday_week_day(6) -> 5;
yesterday_week_day(5) -> 4;
yesterday_week_day(4) -> 3;
yesterday_week_day(3) -> 2;
yesterday_week_day(2) -> 1;
yesterday_week_day(1) -> 7.

%% 获取n天前的时间戳
nth_days_before(Days) when Days > 0 ->
  nth_days_before(time_util:now(), Days).

nth_days_before(Seconds, Days) when Days > 0 ->
  T1 = Seconds - Days * ?ONE_DAY_SEC,
  {DateTime, _} = timestamp_to_datetime(T1),
  local_time2seconds({DateTime, {0,0,0}}).

nth_days_after(Days) when Days >= 0 ->
  nth_days_after(time_util:now(), Days).

nth_days_after(Seconds, Days) when Days >= 0 ->
  T1 = Seconds + Days * ?ONE_DAY_SEC,
  {DayTime, _} = timestamp_to_datetime(T1),
  local_time2seconds({DayTime, {0, 0, 0}}).

nth_hours_after(Hours) when Hours >= 0 ->
  nth_hours_after(time_util:now(), Hours).

nth_hours_after(Seconds, Hours) ->
  Seconds + (Hours * ?ONE_HOUR_SEC).

%% 获取n小时前的时间戳
nth_hours_before(Hours) when Hours > 0 ->
  nth_hours_before(time_util:now(), Hours).

nth_hours_before(Seconds, Hours) when Hours > 0 ->
  Seconds - Hours * 60 * 60.

%% 将本地时间转为时间戳
local_time2seconds(DayTime) ->
  [Time] = calendar:local_time_to_universal_time_dst(DayTime),
  calendar:datetime_to_gregorian_seconds(Time) - 62167219200.

%% 获取当天年月日tuple
today_tuple() ->
  {Today, _} = timestamp_to_datetime(time_util:now() - 3600 * ?SPAN_POINT),
  Today.

%% 获取n天后的年月日tuple
today_tuple(N) ->
  {Today, _} = timestamp_to_datetime(time_util:now() + N * ?ONE_DAY_SEC - 3600 * ?SPAN_POINT),
  Today.

% 判断两个时间的大小
is_less({H1,M1,S1}, {H2,M2,S2}) ->
  T1 = H1*3600 + M1*60 + S1,
  T2 = H2*3600 + M2*60 + S2,
  T1 < T2.

%% 判断时间是否为整点
is_point(Timestamp) ->
  Timestamp rem 3600 =:= 0.

%% 判断时间段是否跨越整点
span_point(Start, End) when is_integer(Start), is_integer(End) ->
  Clock = trunc(End / 3600) * 3600,
  Start < Clock.

get_diff_days({Y1, M1, D1}, {Y2, M2, D2})->
  Days1 = calendar:date_to_gregorian_days(Y1, M1, D1),
  Days2 = calendar:date_to_gregorian_days(Y2, M2, D2),
  abs(Days2-Days1);
get_diff_days(Sec1, Sec2) ->
  {Date1, _} = timestamp_to_datetime(Sec1),
  {Date2, _} = timestamp_to_datetime(Sec2),
  get_diff_days(Date1, Date2).

%% 判断时间点是否在时间范围内
%% 1. 先格式化时间戳
%% 2. 做时间比较
in_period([{A,B}|Rest], TimeStamp) when is_list(A) ->
  A1 = string_to_ts(A),
  B1 = string_to_ts(B),
  in_period([{A1, B1} | Rest], TimeStamp);
in_period([{A,B}|Rest], Timestamp) when not is_integer(A) ->
  A1 = local_time2seconds(A),
  B1 = local_time2seconds(B),
  in_period([{A1,B1}|Rest], Timestamp);
in_period([], _Timestamp) -> false;
in_period([{A,B}|Rest], Timestamp) ->
  case A =< Timestamp andalso B >= Timestamp of
    true -> true;
    false -> in_period(Rest, Timestamp)
  end.

%% 1s = 1000 ms
sleep(MS) ->
  receive
  after MS ->
          ok
  end.

date() ->
  {{Y, M, D},_} = time_util:local_time(),
  {Y, M, D}.
date_before(N) ->
  {Date, _} = timestamp_to_datetime(time_util:now() - N * ?ONE_DAY_SEC),
  Date.

%%从linux系统获取时区
time_zone() ->
  case config:get(game_svr, time_zone, nowarn) of
    {ok, TimeZone} ->
      TimeZone;
    _ ->
      TimeZone = time_zone__(),
      application:set_env(game_svr, time_zone, TimeZone),
      TimeZone
  end.

time_zone__() ->
  TimeZoneStr = string:trim(os:cmd("date +'%z'")),
  TimeZoneInt = list_to_integer(TimeZoneStr),
  TimeZone    = trunc(TimeZoneInt/100),
  TimeZone.