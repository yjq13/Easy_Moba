using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    public class SelfAllTarget : TargetBase
    {
        public override void Init()
        {
            base.Init();
            m_gameplayer = GameFacade.GetCurrentCardGame().GetSelfCampPlayers();
        }

        public override bool CheckFinishChoose()
        {
            return true;
        }

        public override void Reset()
        {
            m_gameplayer = GameFacade.GetCurrentCardGame().GetSelfCampPlayers();
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