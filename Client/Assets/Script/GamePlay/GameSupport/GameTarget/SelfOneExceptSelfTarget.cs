using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    public class SelfOneExceptSelfTarget : SelfOneTarget
    {
        public override List<GamePlayer> GetCanChooseTarget()
        {
            List<GamePlayer> players = new List<GamePlayer>();
            foreach (var player in GameFacade.GetCurrentCardGame().GetSelfCampPlayers())
            {
                if(player != GameFacade.GetCurrentCardGame().GetMyPlayer())
                {
                    players.Add(player);
                }
            }
            return players;
        }
    }
}
