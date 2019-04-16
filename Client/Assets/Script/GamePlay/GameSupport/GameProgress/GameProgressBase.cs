using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamePlay
{
    public abstract class GameProgressBase
    {
        protected GameProgressBase()
        {
            InitProgress();
        }

        protected void EndProgress()
        {
            OnEndProgress();
            GameFacade.GetCurrentCardGame().StartNextProgress();
        }

        private void InitProgress()
        {
            OnInitProgress();
        }

        public void StartProgress()
        {
            OnStartProgress();
        }

        public void SetProgressEnd()
        {
            EndProgress();
        }

        public void KillCurrentRound()
        {
            OnClearGameProgress();
            GameFacade.GetCurrentCardGame().JumpToLastProgress();
        }

        public abstract void OnInitProgress();
        public abstract void OnStartProgress();
        public abstract void OnEndProgress();
        public abstract void OnClearGameProgress();
        public abstract ProgressType GetProgressState();
    }
}

