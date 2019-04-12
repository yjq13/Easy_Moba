using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{

    public class EffectBase
    {
        private List<GamePlayer> m_targetList = null;

        public void SetTarget(List<GamePlayer> targets)
        {
            m_targetList = targets;
        }

        public void TakeEffect()
        {
            if(m_targetList == null)
            {
                return;
            }
            foreach(var target in m_targetList)
            {
                OnTakeEffect(target);
            }

        }

        protected virtual void OnTakeEffect(GamePlayer target)
        {

        }
    }
}
