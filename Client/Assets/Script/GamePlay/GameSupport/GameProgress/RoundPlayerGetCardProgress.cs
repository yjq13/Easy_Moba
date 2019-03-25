using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

namespace GamePlay
{
    public class RoundPlayerGetCardProgress : GameProgressBase
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
                //deal with Get Card
                if (player.Role != null)
                {
                    player.Role.GetCard(3);
                }
            }
            SetProgressEnd();
        }

        public override void OnClearGameProgress()
        {
            
        }
    }
}

