using System.Collections.Generic;
using Common;

namespace GamePlay
{
    public class SelfTarget : TargetBase
    {
        public override void SetGameTarget(GamePlayer player)
        {
            base.SetGameTarget(player);
            m_gameplayer.Add(self_player);
        }

        public override bool CheckFinishChoose()
        {
            return true;
        }

        public override bool NeedChooseTarget()
        {
            return false;
        }


        public override List<GamePlayer> GetCanChooseTarget()
        {
            return m_gameplayer;
        }
    }
}
