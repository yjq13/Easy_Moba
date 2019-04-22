using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    class OppoOneTarget : TargetBase
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
            CampType m_camp = self_player.CampType;
            CampType oppo_camp = GameFacade.GetCurrentCardGame().GetOppoType(m_camp);
            return GameFacade.GetCurrentCardGame().GetGamePlayersByCamp(oppo_camp);
        }

        public override bool NeedChooseTarget()
        {
            return true;
        }
    }
}
