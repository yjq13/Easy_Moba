using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            if(GameManager.Instance.GetCurrentAuthorizationPlayer() != GamePlayer.GAME_MANAGER)
            {
                GamePlayer player = GameManager.Instance.GetCurrentAuthorizationPlayer();
                //deal with ActionPoint
                if(player.Role != null)
                {
                    player.Role.ChangeActionPoint(3);
                    //deal with Buff
                    //to do
                }
            }
        }

        public override void OnClearGameProgress()
        {

        }
    }
}

