using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    class AllExceptSelfTarget : AllTarget
    {
        public override void SetGameTarget(GamePlayer player)
        {
            base.SetGameTarget(player);
            List<GamePlayer> players = new List<GamePlayer>();
            foreach(var player_target in GameFacade.GetCurrentCardGame().GetAllGamePlayers())
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
