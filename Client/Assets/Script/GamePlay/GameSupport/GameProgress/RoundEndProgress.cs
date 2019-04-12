using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

namespace GamePlay
{
    public class RoundEndProgress : GameProgressBase
    {
        public override void OnEndProgress()
        {
            GameFacade.GetCurrentCardGame().GetBackAuthorization();
        }

        public override void OnInitProgress()
        {

        }

        public override void OnStartProgress()
        {
            SetProgressEnd();
        }

        public override void OnClearGameProgress()
        {

        }

        public override ProgressType GetProgressState()
        {
            return ProgressType.RoundEnd;
        }
    }
}