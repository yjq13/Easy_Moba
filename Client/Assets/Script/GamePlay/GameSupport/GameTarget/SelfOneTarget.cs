using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    public class SelfOneTarget : TargetBase
    {
        public override bool CheckFinishChoose()
        {
            if(m_gameplayer.Count == 1)
            {
                return true;
            }
            return false;
        }

        public override List<GamePlayer> GetCanChooseTarget()
        {
            return GameFacade.GetCurrentCardGame().GetSelfCampPlayers();
        }

        public override bool NeedChooseTarget()
        {
            return true;
        }
    }
}
