using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamePlay
{
    interface IGameProgress
    {
        void StartProgress();
        void EndProgress();
    }

    public abstract class GameProgressBase : IGameProgress
    {
        private static List<GameProgressBase> m_registerGameProgress = new List<GameProgressBase>();
        private static int m_static_current_index = 0;

        protected GameProgressBase()
        {
            m_registerGameProgress.Add(this);
            InitProgress();
        }

        public static void ClearGameProgress()
        {
            foreach(var progress in m_registerGameProgress)
            {
                progress.OnClearGameProgress();   
            }
        }

        public void EndProgress()
        {
            OnEndProgress();
            m_static_current_index++;
            if(m_static_current_index >= m_registerGameProgress.Count)
            {
                m_static_current_index = 0;
            }

            m_registerGameProgress[m_static_current_index].StartProgress();
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

        public abstract void OnInitProgress();
        public abstract void OnStartProgress();
        public abstract void OnEndProgress();
        public abstract void OnClearGameProgress();
    }
}

