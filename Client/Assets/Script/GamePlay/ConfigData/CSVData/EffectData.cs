using System;
using System.Collections.Generic;
using Common;
namespace GamePlay 
{
    public struct EffectInfoData 
    {
        public GameTargetType TargetType;
        public EFFECT_TYPE EffectID;
        public ELEMENT_PROPERTY elementPtoprtty;
        public string EffectParam1;
        public string EffectParam2;

        public EffectInfoData(GameTargetType targetType, EFFECT_TYPE effectID,string param1,string param2,ELEMENT_PROPERTY element = ELEMENT_PROPERTY.NONE)
        {
            TargetType = targetType;
            EffectID = effectID;
            EffectParam1 = param1;
            EffectParam2 = param2;
            elementPtoprtty = element;
        }
        public static List<EffectInfoData> ParseData(string[] list_array)
        {
            List <EffectInfoData> effect_List = new List<EffectInfoData>();
            for (int i = 0;i<list_array.Length;i++)
            {
                GameTargetType _TargetType = GameTargetType.NONE;
                EFFECT_TYPE _EffectID = EFFECT_TYPE.None;
                string _EffectParam1 = "";
                string _EffectParam2 = "";
                switch (i % 4)
                {
                    case 0:
                        {
                            _TargetType = (GameTargetType)Enum.Parse(typeof(GameTargetType), list_array[i]);
                            break;
                        }
                    case 1:
                        {
                            _EffectID = (EFFECT_TYPE)Enum.Parse(typeof(EFFECT_TYPE), list_array[i]);
                            break;
                        }
                    case 2:
                        {
                            _EffectParam1 = list_array[i];
                            break;
                        }
                    case 3:
                        {
                            _EffectParam2 = list_array[i];
                            break;
                        }
                }
                if(i%4 == 3 || i == list_array.Length -1)
                {
                    effect_List.Add(new EffectInfoData(_TargetType, _EffectID, _EffectParam1,_EffectParam2));
                }
            }
            return effect_List;
        }
    }

    public class EffectData : CSVBaseData
    {
        public override string GetPrimaryKey()
        {
            throw new NotImplementedException();
        }

        public override void ParseData(long index, int fieldCount, string[] headers, string[] values)
        {
            throw new NotImplementedException();
        }
    }
}
