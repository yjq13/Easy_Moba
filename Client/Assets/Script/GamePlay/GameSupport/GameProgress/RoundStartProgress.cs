using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

namespace GamePlay
{
    public class RoundStartProgress : GameProgressBase
    {
        public override void OnEndProgress()
        {

        }

        public override void OnInitProgress()
        {
            
        }

        public override void OnStartProgress()
        {
            if (GameFacade.GetCurrentCardGame().GetCurrentAuthorizationPlayer() != GamePlayer.GAME_MANAGER)
            {
                GamePlayer player = GameFacade.GetCurrentCardGame().GetCurrentAuthorizationPlayer();
                //deal with ActionPoint
                if(player.Role != null)
                {
                    player.Role.ChangeActionPoint(3);
                    //deal with Buff
                    //to do
                }
            }
            SetProgressEnd();
        }

        public override void OnClearGameProgress()
        {

        }
    }
}

