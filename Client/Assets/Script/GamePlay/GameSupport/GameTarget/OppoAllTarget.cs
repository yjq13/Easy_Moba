using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    class OppoAllTarget : TargetBase
    {
        public override void SetGameTarget(GamePlayer player)
        {
            base.SetGameTarget(player);
            CampType m_camp = self_player.CampType;
            CampType oppo_camp = GameFacade.GetCurrentCardGame().GetOppoType(m_camp);
            m_gameplayer = GameFacade.GetCurrentCardGame().GetGamePlayersByCamp(oppo_camp);
        }

        public override bool CheckFinishChoose()
        {
            return true;
        }

        public override List<GamePlayer> GetCanChooseTarget()
        {
            return m_gameplayer;
        }

        public override bool NeedChooseTarget()
        {
            return false;
        }
    }
}
