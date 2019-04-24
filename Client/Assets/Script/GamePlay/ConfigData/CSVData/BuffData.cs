using System;
using System.Collections.Generic;
using Common;
namespace GamePlay
{
    public class BuffData : CSVBaseData
    {
        public string Buff_ID;
        public Buff_NOTIFY_TYPE TriggerTime;
        public string TriggerParam;
        public Buff_NOTIFY_TYPE CutCountTime;
        public List<EffectInfoData> EffectList;
        public bool Compositable;

        public override string GetPrimaryKey()
        {
            return Buff_ID.ToString();
        }

        public override void ParseData(long index, int fieldCount, string[] headers, string[] values)
        {
            Buff_ID = ReadString("Type", headers, values, "");
            string[] doubleValue = ReadDoubleValue("TriggerTime", headers, values, "");
            TriggerTime = (Buff_NOTIFY_TYPE)Enum.Parse(typeof(Buff_NOTIFY_TYPE), doubleValue[0]);
            TriggerParam = doubleValue[1];
            CutCountTime = (Buff_NOTIFY_TYPE)Enum.Parse(typeof(Buff_NOTIFY_TYPE), ReadString("CutCountTime", headers, values, ""));
            string[] effect_data = ReadStringArray("Effects", headers, values, "");
            EffectList = EffectInfoData.ParseData(effect_data);
        }
    }
}
