using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    public class SelfAllTarget : TargetBase
    {
        public override void SetGameTarget(GamePlayer player)
        {
            base.SetGameTarget(player);
            m_gameplayer = GameFacade.GetCurrentCardGame().GetGamePlayersByCamp(self_player.CampType);
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