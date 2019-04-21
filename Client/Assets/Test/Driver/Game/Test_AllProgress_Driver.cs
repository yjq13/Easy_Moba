using Common;
using GamePlay;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Test
{
    class Test_AllProgress_Driver : DriverBase
    {
        public override void ClearContext()
        {
            ConfigDataManager.Instance.Cleanup();
        }

        public override void InitContext()
        {
            ConfigDataManager.Instance.LoadCSV<RoleData>(ResourceIDDef.GAME_ROLE_CONFIG);
        }

        public override void Test()
        {
            CardSet cardSet_playerOne = new CardSet();
            CardSet cardSet_playerTwo = new CardSet();
            List<uint> card_id_list_one = new List<uint> { 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 14, 19, 27, 29, 8, 2, 2, 2, 2, 2, 2, 7, 7, 7, 9, 9, 9, 23, 26, 11 };
            List<uint> card_id_list_tow = new List<uint> { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 2, 2, 2, 2, 2, 2, 6, 18, 18, 18, 8, 8, 8, 20, 7, 7, 7, 24, 26, 12 };
            cardSet_playerOne.InitCardSet(card_id_list_one);
            cardSet_playerTwo.InitCardSet(card_id_list_tow);

            RoleArcher archer = new RoleArcher(cardSet_playerOne);
            GamePlayer player_one = new GamePlayer(archer, 1);
            RoleMage mage = new RoleMage(cardSet_playerTwo);
            GamePlayer player_two = new GamePlayer(mage, 2);

            List<GamePlayer> players = new List<GamePlayer>
            {
                player_one,
                player_two
            };

            CardGame Card_Game = new CardGame();
            Card_Game.OnAwake();

            Card_Game.SetGameInfo(players, player_one);
            Assert.IsTrue(Card_Game.GetCurrentAuthorizationPlayer().PlayerID == 1);
            Assert.IsTrue(Card_Game.GetCurrentAuthorizationPlayer().Role.CurrentCanUseCardList.Count == 3);
            Card_Game.GetCurrenProgress().SetProgressEnd();
            Assert.IsTrue(Card_Game.GetCurrentAuthorizationPlayer().PlayerID == 2);
            Assert.IsTrue(Card_Game.GetCurrentAuthorizationPlayer().Role.CurrentCanUseCardList.Count == 3);
            Card_Game.GetCurrenProgress().SetProgressEnd();
            Assert.IsTrue(Card_Game.GetCurrentAuthorizationPlayer().PlayerID == 1);
            Assert.IsTrue(Card_Game.GetCurrentAuthorizationPlayer().Role.CurrentCanUseCardList.Count == 6);
            Card_Game.GetCurrenProgress().SetProgressEnd();
            Assert.IsTrue(Card_Game.GetCurrentAuthorizationPlayer().PlayerID == 1);
            Assert.IsTrue(Card_Game.GetCurrentAuthorizationPlayer().Role.CurrentCanUseCardList.Count == 9);
            Card_Game.GetCurrenProgress().SetProgressEnd();
            Assert.IsTrue(Card_Game.GetCurrentAuthorizationPlayer().PlayerID == 2);
            Assert.IsTrue(Card_Game.GetCurrentAuthorizationPlayer().Role.CurrentCanUseCardList.Count == 6);
        }
    }
}

