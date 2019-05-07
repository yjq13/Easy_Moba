-module(random_util).

-export([next/0, next/1, next/2, rand_from_list/1, get_random_seed/0,
  shuffle_list/1, shuffle_list/2, round/1, random_with_weight/1]).

-define(MAX_SEED, 10000).

next() -> 
  rand:uniform().

next(M) ->
  rand:uniform(M).

next(Min, Max) ->
  rand:uniform(Max - Min + 1) + Min - 1.

rand_from_list(List) ->
  Len = length(List),
  lists:nth(next(Len), List).

get_random_seed() ->
  next(?MAX_SEED).

shuffle_list(List) ->
  [X || {_,X} <- lists:sort([{rand:uniform(), N} || N <- List])].

%%随机取n个
shuffle_list(List, N) ->
  ShuffleList = shuffle_list(List),
  lists:sublist(ShuffleList, N).

round(Num) when Num >= 0 ->
  Integer = trunc(Num),
  Decimal = Num - Integer,
  case Decimal > next() of
    true ->
      Integer + 1;
    false ->
      Integer
  end.

%%[{V, Weight}...]
random_with_weight(List) ->
  WeightList = [Weight || {_, Weight} <- List],
  Sum        = lists:sum(WeightList),
  Rand       = next() * Sum,
  random_with_weight(List, Rand).
random_with_weight([{Data, Weight} | List], Rand) ->
  case Rand =< Weight of
    true -> Data;
    false -> random_with_weight(List, Rand - Weight)
  end.
