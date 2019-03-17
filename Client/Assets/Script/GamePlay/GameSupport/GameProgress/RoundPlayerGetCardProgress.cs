using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamePlay
{
    public abstract class RoundPlayerGetCardProgress : GameProgressBase
    {
        public override void OnEndProgress()
        {
            
        }

        public override void OnInitProgress()
        {

        }

        public override void OnStartProgress()
        {
            if (GameManager.Instance.GetCurrentAuthorizationPlayer() != GamePlayer.GAME_MANAGER)
            {
                GamePlayer player = GameManager.Instance.GetCurrentAuthorizationPlayer();
                //deal with Get Card
                if (player.Role != null)
                {
                    player.Role.GetCard(3);
                }
            }
        }

        public override void OnClearGameProgress()
        {
            
        }
    }
}

