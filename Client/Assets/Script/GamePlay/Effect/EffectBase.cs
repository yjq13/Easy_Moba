﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{

    public abstract class EffectBase
    {
        public void TakeEffect(List<GamePlayer> targets, params object[] param)
        {
            if(targets == null)
            {
                return;
            }
            foreach(var target in targets)
            {
                OnTakeEffect(target, param);
            }

        }

        protected abstract void OnTakeEffect(GamePlayer target, params object[] param);
    }
}
