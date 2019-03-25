using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace GamePlay
{
    public static class GameFacade
    {
        public static MyGameBase CurrentGame;

        public static void SetCurrentGame(MyGameBase game)
        {
            CurrentGame = game;
        }

        public static CardGame GetCurrentCardGame()
        {
            if(CurrentGame.GameType == GameType.CARD_GAME)
            {
                return CurrentGame as CardGame;
            }
            return null;
        }
    }
}
