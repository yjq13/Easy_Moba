using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    class CountUtil
    {
        public int CalcCount(params object[] count_params)
        {
            firstParam = count_params[0].ToString();
            if (IsNum(firstParam))
            {
                StringConverter.ToInt(firstParam, 0);
            }
            else
            {
                // !!! 暂未实现
                switch (firstParam)
                {
                    case "sp_card_cost_magic_point_cnt":
                        return 0;
                        break;
                    case "magic_point_cnt":
                        return 0;
                        break;
                    default:
                        Debug.LogError("calc count error with first param: " + firstParam);
                        break;
                }
            }
        }

        public bool IsNum(string str)
        {
            char[] chars = new char[str.Length];
            chars =str.ToCharArray();
            for(int i = 0; i < str.Length; i ++)
            {
                if (chars[i] < 48 || chars[i] > 57)
                    return false;
            }
            return true;
        }

    }
}
