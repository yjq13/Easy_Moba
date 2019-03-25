using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Common
{
    class GameEngine : MonoBehaviour
    {
        public static GameEngine instance;

        private void Awake()
        {
            instance = this;
        }

        public void LoadGame()
        {

        }
    }
}
