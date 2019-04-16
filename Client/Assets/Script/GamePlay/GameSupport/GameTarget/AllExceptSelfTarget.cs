using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    class AllExceptSelfTarget : AllTarget
    {
        public override void Init()
        {
            base.Init();
            List<GamePlayer> players = new List<GamePlayer>();
            foreach(var player in GameFacade.GetCurrentCardGame().GetAllGamePlayers())
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
            foreach (var player in GameFacade.GetCurrentCardGame().GetAllGamePlayers())
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
