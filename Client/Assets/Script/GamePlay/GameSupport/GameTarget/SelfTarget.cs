using System.Collections.Generic;
using Common;

namespace GamePlay
{
    public class SelfTarget : TargetBase
    {
        public override void Init()
        {
            base.Init();
            m_gameplayer.Add(GameFacade.GetCurrentCardGame().GetMyPlayer());
        }

        public override bool CheckFinishChoose()
        {
            return true;
        }

        public override void Reset()
        {
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
