using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common;

namespace GamePlay
{
    public enum GameType
    {
        NONE = 0,
        CARD_GAME = 1
    }

    public class MyGameBase : GameBase
    {
        public GameType GameType = GameType.NONE;
        protected EventDispatcher m_GameEventDispatcher;
        public EventDispatcher GameEventDispatcher
        {
            get { return m_GameEventDispatcher; }
        }

        public override void OnAwake()
        {
            GameFacade.SetCurrentGame(this);
        }

        public void Init()
        {
            m_GameEventDispatcher = new EventDispatcher();
        }
    }
}
