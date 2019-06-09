using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace GamePlay
{
    public enum ProgressType
    {
        CalculateSpeed = 0,
        RoundStart,
        RoundPlayerGetCard,
        RoundPlaying,
        RoundEnd,
        ExtraPlayerOperation = 100
    }
    public class GameProgressManager
    {
        private  List<GameProgressBase> m_registerGameProgress = new List<GameProgressBase>();
        private int m_static_current_index = 0;
        public  uint RoundCount { get; private set; }
        public  uint MaxRoundCount = 9999;
        private CalculateSpeedProgress start_progress;

        public void Init()
        {
            CalculateSpeedProgress progress_cal = new CalculateSpeedProgress();
            RoundStartProgress progress_Start = new RoundStartProgress();
            RoundPlayerGetCardProgress progress_GetCard = new RoundPlayerGetCardProgress();
            RoundPlayingProgress progress_Playing = new RoundPlayingProgress();
            RoundEndProgress progress_End = new RoundEndProgress();
            start_progress = progress_cal;
        }

        public void StartProgress()
        {
            start_progress.FirstStartProgress();
        }

        public void RegisterProgress(GameProgressBase progress)
        {
            m_registerGameProgress.Add(progress);
        }


        public void StartNextProgress()
        {
            m_static_current_index++;
            if (m_static_current_index >= m_registerGameProgress.Count)
            {
                m_static_current_index = 0;
                RoundCount++;
                if (RoundCount > MaxRoundCount)
                {
                    return;
                }
            }

            m_registerGameProgress[m_static_current_index].StartProgress();
        }

        public void JumpToLastProgress()
        {
            m_registerGameProgress[m_static_current_index].SetProgressEnd();
            m_static_current_index = m_registerGameProgress.Count - 1;
            m_registerGameProgress[m_static_current_index].StartProgress();
        }


        public void Cleanup()
        {
            m_registerGameProgress.Clear();
            start_progress = null;
        }

        public GameProgressBase GetCurrenProgress()
        {
            if (m_registerGameProgress.Count > 0)
            {
                return m_registerGameProgress[m_static_current_index];
            }
            else
            {
                return null;
            }

        }

        public CalculateSpeedProgress GetSpeedProgress()
        {
            return start_progress;
        }


        public void ResetGameProgress()
        {
            RoundCount = 1;
            m_static_current_index = 0;
            foreach (var progress in m_registerGameProgress)
            {
                progress.OnClearGameProgress();
            }
            m_registerGameProgress.Clear();
        }
    }
}
