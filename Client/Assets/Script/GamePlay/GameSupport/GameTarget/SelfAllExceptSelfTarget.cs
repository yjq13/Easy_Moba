using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    class SelfAllExceptSelfTarget : SelfAllTarget
    {
        public override void Init()
        {
            base.Init();
            List<GamePlayer> players = new List<GamePlayer>();
            foreach (var player in GameFacade.GetCurrentCardGame().GetSelfCampPlayers())
            {
                if (player != GameFacade.GetCurrentCardGame().GetMyPlayer())
                {
                    players.Add(player);
                }
            }
            m_gameplayer = players;
        }

        public override void Reset()
        {
            List<GamePlayer> players = new List<GamePlayer>();
            foreach (var player in GameFacade.GetCurrentCardGame().GetSelfCampPlayers())
            {
                if (player != GameFacade.GetCurrentCardGame().GetMyPlayer())
                {
                    players.Add(player);
                }
            }
            m_gameplayer = players;
        }
    }
}
