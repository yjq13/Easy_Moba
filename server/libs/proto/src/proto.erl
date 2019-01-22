-module(proto).

%% API exports
-export([get_enums/2
        , get_fields/2
        , check_valid_enum/3
        , check_valid_enum/4
        , check_valid_field/3
        , get_enum_value/3
        , is_valid/3
        ]).

-include("err_code.hrl").
-include("gpb.hrl").
%%====================================================================
%% API functions
%%====================================================================
get_enums(Proto, EnumType) ->
	Mod = list_to_atom(lists:concat([Proto, "_pb"])),
	Def = Mod:fetch_enum_def(EnumType),
	[V || {V, _No} <- Def].

get_fields(Proto, MsgType) ->
  Mod = list_to_atom(lists:concat([Proto, "_pb"])),
  Def = Mod:find_msg_def(MsgType),
  [Field#field.name || Field <- Def].

check_valid_enum(Proto, EnumType, Enum) ->
  check_valid_enum(Proto, EnumType, Enum, ?ERR_PROTO_ENUM).

check_valid_enum(Proto, EnumType, Enum, ErrCode) ->
  Enums = get_enums(Proto, EnumType),
  case lists:member(Enum, Enums) of
    true -> ok;
    false -> ErrCode
  end.

is_valid(Proto, EnumType, Enum) ->
  Enums = get_enums(Proto, EnumType),
  lists:member(Enum, Enums).

check_valid_field(Proto, MsgType, Field) ->
  Fields = get_fields(Proto, MsgType),
  case lists:member(Field, Fields) of
    true -> ok;
    false -> ?ERR_PROTO_FIELD
  end.

get_enum_value(Proto, EnumType, Symbol) ->
  Mod = list_to_atom(lists:concat([Proto, "_pb"])),
  Fun = list_to_atom(lists:concat([enum_value_by_symbol_, EnumType])),
  Mod:Fun(Symbol).