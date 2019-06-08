using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace GamePlay
{
    public static class GameFacade
    {
        private static MyGameBase m_currentGame;

        public static void SetCurrentGame(MyGameBase game)
        {
            m_currentGame = game;
        }

        public static CardGame GetCurrentCardGame()
        {
            if (m_currentGame.GameType == GameType.CARD_GAME)
            {
                return m_currentGame as CardGame;
            }
            return null;
        }

        public static UISceneBase GetCurrentUIScene()
        {
            if(m_currentGame != null)
            {
                return m_currentGame.CurrentUIScene;
            }
            return null;
        }
    }
}
