using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using GamePlay;

namespace Common
{
    class GameEngine : MonoBehaviour
    {
        public static GameEngine instance;
        private MyGameBase m_CurrentGame;

        private void Awake()
        {
            instance = this;
        }

        public void LoadGame()
        {

        }

        public void StartGame<T>()
        {
             m_CurrentGame = (MyGameBase)Activator.CreateInstance(typeof(T));
        }

        public void RunOneFrame()
        {
            if(m_CurrentGame != null)
            {
                m_CurrentGame.OnUpdate();
            }
        }
    }
}
