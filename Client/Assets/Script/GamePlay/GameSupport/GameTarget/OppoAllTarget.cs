using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    class OppoAllTarget : TargetBase
    {
        public override void Init()
        {
            base.Init();
            m_gameplayer = GameFacade.GetCurrentCardGame().GetOppoCampPlayers();
        }

        public override void Reset()
        {
            m_gameplayer = GameFacade.GetCurrentCardGame().GetOppoCampPlayers();
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
