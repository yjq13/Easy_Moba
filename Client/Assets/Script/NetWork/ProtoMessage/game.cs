// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: game.proto

#pragma warning disable 1591, 0612, 3021

namespace easy_moba
{

    [global::ProtoBuf.ProtoContract()]
    public partial class user
    {
        public user()
        {
            uin = "";
            union_id = "";
            name = "";
            login_ip = "";
            red_dots = new global::System.Collections.Generic.List<red_dot>();
            settings = new global::System.Collections.Generic.List<setting>();
            avatar = "";
            desc = "";
            OnConstructor();
        }

        partial void OnConstructor();

        [global::ProtoBuf.ProtoMember(1)]
        public ulong uid { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string uin { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string union_id { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public uint svr_id { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public uint version { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string name { get; set; }

        [global::ProtoBuf.ProtoMember(7)]
        public uint level { get; set; }

        [global::ProtoBuf.ProtoMember(8)]
        public uint exp { get; set; }

        [global::ProtoBuf.ProtoMember(9)]
        public uint login_ts { get; set; }

        [global::ProtoBuf.ProtoMember(10)]
        [global::System.ComponentModel.DefaultValue("")]
        public string login_ip { get; set; }

        [global::ProtoBuf.ProtoMember(11)]
        public uint logout_ts { get; set; }

        [global::ProtoBuf.ProtoMember(12)]
        public update_ts update_ts { get; set; }

        [global::ProtoBuf.ProtoMember(13)]
        public bool is_online { get; set; }

        [global::ProtoBuf.ProtoMember(14)]
        public global::System.Collections.Generic.List<red_dot> red_dots { get; private set; }

        [global::ProtoBuf.ProtoMember(15)]
        public global::System.Collections.Generic.List<setting> settings { get; private set; }

        [global::ProtoBuf.ProtoMember(16, IsPacked = true)]
        public uint[] tutorials { get; set; }

        [global::ProtoBuf.ProtoMember(17)]
        public t_lang lang { get; set; }

        [global::ProtoBuf.ProtoMember(18)]
        public int time_zone { get; set; }

        [global::ProtoBuf.ProtoMember(19)]
        [global::System.ComponentModel.DefaultValue("")]
        public string avatar { get; set; }

        [global::ProtoBuf.ProtoMember(20)]
        public uint frame { get; set; }

        [global::ProtoBuf.ProtoMember(21)]
        public t_gender gender { get; set; }

        [global::ProtoBuf.ProtoMember(22)]
        public uint country { get; set; }

        [global::ProtoBuf.ProtoMember(23)]
        public uint city { get; set; }

        [global::ProtoBuf.ProtoMember(24)]
        [global::System.ComponentModel.DefaultValue("")]
        public string desc { get; set; }

        [global::ProtoBuf.ProtoMember(25, IsPacked = true)]
        public uint[] runtime_funs { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class update_ts
    {
        public update_ts()
        {
            OnConstructor();
        }

        partial void OnConstructor();

        [global::ProtoBuf.ProtoMember(1)]
        public uint last_hourly_ts { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public uint last_daily_ts { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public uint last_weekly_ts { get; set; }

    }

}

#pragma warning restore 1591, 0612, 3021
