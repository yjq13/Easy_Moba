using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamePlay
{

    public abstract class GameProgressBase
    {
        private static List<GameProgressBase> m_registerGameProgress = new List<GameProgressBase>();
        private static int m_static_current_index = 0;
        public static uint RoundCount { get; private set; }
        public static uint MaxRoundCount = 9999;

        protected GameProgressBase()
        {
            m_registerGameProgress.Add(this);
            InitProgress();
        }

        public static GameProgressBase GetCurrenProgress()
        {
            if(m_registerGameProgress.Count > 0)
            {
                return m_registerGameProgress[m_static_current_index];
            }
            else
            {
                return null;
            }

        }


        public static void ResetGameProgress()
        {
            RoundCount = 1;
            m_static_current_index = 0;
            foreach (var progress in m_registerGameProgress)
            {
                progress.OnClearGameProgress();   
            }
            m_registerGameProgress.Clear();
        }

        protected void EndProgress()
        {
            OnEndProgress();
            m_static_current_index++;
            if(m_static_current_index >= m_registerGameProgress.Count)
            {
                m_static_current_index = 0;
                RoundCount++;
                if(RoundCount > MaxRoundCount)
                {
                    return;
                }
            }

            m_registerGameProgress[m_static_current_index].StartProgress();
        }

        private void InitProgress()
        {
            OnInitProgress();
        }

        protected void StartProgress()
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
            m_static_current_index = 0;
            m_registerGameProgress[m_static_current_index].StartProgress();
        }

        public abstract void OnInitProgress();
        public abstract void OnStartProgress();
        public abstract void OnEndProgress();
        public abstract void OnClearGameProgress();
    }
}

