using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    public abstract class BuffBase
    {

        public abstract void OnTriggerBuff(params object[] param);
        public abstract uint GetInterestedTrigger();
    }
}
