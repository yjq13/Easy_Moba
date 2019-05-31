using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Common.Util
{
    public static class BitArray
    {
       public static bool HasFlag(uint bitArray,uint flag)
        {
            return (bitArray & flag) != 0;
        }
    }
}
