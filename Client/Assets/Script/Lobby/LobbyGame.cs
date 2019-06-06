using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace GamePlay
{
    public class LobbyGame : MyGameBase
    {
        public override void OnAwake()
        {
            base.OnAwake();
            GameType = GameType.CARD_GAME;
            CurrentUIScene = new UILobbyScene();
        }
    }
}
