using System;
using UnityEngine;
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
        public UISceneBase CurrentUIScene
        {
            get;protected set;
        }
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

        public void SetUIScene()
        {
            CurrentUIScene = (UISceneBase)Activator.CreateInstance(GetUISceneType());
            if (UISceneBase.UIRoot == null)
            {
                GameObject root = GameObject.Find("UIRoot");
                if (root != null)
                {
                    UISceneBase.SetUIRoot(root.transform);
                }
            }
        }

        public Type GetUISceneType()
        {
            return typeof(UILobbyScene);
        }
    }
}
