using System.Collections.Generic;
using Common;

namespace GamePlay
{
    public class SelfTarget : Interface_Target
    {
        private List<GamePlayer> m_gameplayer = new List<GamePlayer>()
        {
            GameFacade.GetCurrentCardGame().GetMyPlayer()
        };

        public void AddChooseTarget()
        {
            //do nothing
            return;
        }

        public bool CheckFinishChoose()
        {
            return true;
        }

        public void Clear()
        {
            
        }

        public List<GamePlayer> GetCanChooseTarget()
        {

            return m_gameplayer;
        }

        public List<GamePlayer> GetChoosedTarget()
        {
            return null;
        }

        public bool NeedChooseTarget()
        {
            return false;
        }
    }
}
