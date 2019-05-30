using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Common;

namespace GamePlay
{
    public class CountUtil
    {
        public static int CalcCount(params object[] count_params)
        {
            string firstParam = count_params[0].ToString();
            if (IsNum(firstParam))
            {
                StringConverter.ToInt(firstParam, 0);
            }
            else
            {
                // !!! 暂未实现
                switch (firstParam)
                {
                    case "NONE":
                        return StringConverter.ToInt(count_params[1].ToString());
                    case "sp_card_cost_magic_point_cnt":
                        return 0;
                    case "magic_point_cnt":
                        return 0;
                    default:
                        Debug.LogError("calc count error with first param: " + firstParam);
                        break;
                }
            }
            return -1;
        }

        public static bool IsNum(string str)
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
