using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamePlay
{
    public class RoundPlayingProgress : GameProgressBase
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
                //GameManager.Instance.StartCoroutine(UseCardTimeCounting());
            }
        }

        IEnumerator UseCardTimeCounting()
        {
            yield return new WaitForSeconds(30);
        }




        public override void OnClearGameProgress()
        {

        }
    }
}

