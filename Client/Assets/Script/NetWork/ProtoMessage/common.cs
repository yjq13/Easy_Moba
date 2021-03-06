// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: common.proto

#pragma warning disable 1591, 0612, 3021

namespace easy_moba
{

    [global::ProtoBuf.ProtoContract()]
    public partial class red_dot
    {
        public red_dot()
        {
            OnConstructor();
        }

        partial void OnConstructor();

        [global::ProtoBuf.ProtoMember(1)]
        public t_red_dot type { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public uint id { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public uint start_ts { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public uint expire_ts { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class setting
    {
        public setting()
        {
            OnConstructor();
        }

        partial void OnConstructor();

        [global::ProtoBuf.ProtoMember(1)]
        public t_setting key { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public uint value { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public enum t_platform
    {
        @default = 0,
    }

    [global::ProtoBuf.ProtoContract()]
    public enum t_setting
    {
        sound = 0,
        music = 1,
    }

    [global::ProtoBuf.ProtoContract()]
    public enum t_lang
    {
        cn = 0,
        en = 1,
    }

    [global::ProtoBuf.ProtoContract()]
    public enum t_os
    {
        ios = 0,
        android = 1,
        web = 2,
    }

    [global::ProtoBuf.ProtoContract()]
    public enum t_gender
    {
        none = 0,
        male = 1,
        female = 2,
    }

    [global::ProtoBuf.ProtoContract()]
    public enum t_red_dot
    {
        none = 0,
    }

    [global::ProtoBuf.ProtoContract()]
    public enum t_region
    {
        @default = 0,
    }

}

#pragma warning restore 1591, 0612, 3021
