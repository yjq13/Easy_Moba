using Common;
using GamePlay;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Test
{
    class Test_CalculateSpeedProgress_Driver :Interface_TestDriver
    {
        public void ClearContext()
        {
            ConfigDataManager.Instance.Cleanup();
            GameProgressBase.ClearGameProgress();
        }

        public void InitContext()
        {
            ConfigDataManager.Instance.LoadCSV<RoleData>(ResourceIDDef.GAME_PLAYER_CONFIG);
            GameProgressBase.ClearGameProgress();
        }

        public void Test()
        {
            CardSet cardSet_playerOne = new CardSet();
            CardSet cardSet_playerTwo = new CardSet();
            List<uint> card_id_list_one = new List<uint> { 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 14, 19, 27, 29, 8, 2, 2, 2, 2, 2, 2, 7, 7, 7, 9, 9, 9, 23, 26, 11 };
            List<uint> card_id_list_tow = new List<uint> { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 2, 2, 2, 2, 2, 2, 6, 18, 18, 18, 8, 8, 8, 20, 7, 7, 7, 24, 26, 12 };
            cardSet_playerOne.InitCardSet(card_id_list_one);
            cardSet_playerTwo.InitCardSet(card_id_list_tow);

            RoleArcher archer = new RoleArcher(cardSet_playerOne);
            GamePlayer player_one = new GamePlayer(archer);
            RoleMage mage = new RoleMage(cardSet_playerTwo);
            GamePlayer player_two = new GamePlayer(mage);

            List<GamePlayer> players = new List<GamePlayer>
            {
                player_one,
                player_two
            };

            GameManager.Instance.SetGameInfo(players);

            CalculateSpeedProgress progress = new CalculateSpeedProgress();
            progress.StartProgress();
            Assert.IsTrue(GameManager.Instance.GetCurrentAuthorizationPlayer().PlayerID == 1);
            GameManager.Instance.GetBackAuthorization();
            progress.StartProgress();
            Assert.IsTrue(GameManager.Instance.GetCurrentAuthorizationPlayer().PlayerID == 2);
            GameManager.Instance.GetBackAuthorization();
            progress.StartProgress();
            Assert.IsTrue(GameManager.Instance.GetCurrentAuthorizationPlayer().PlayerID == 1);
            GameManager.Instance.GetBackAuthorization();
            progress.StartProgress();
            Assert.IsTrue(GameManager.Instance.GetCurrentAuthorizationPlayer().PlayerID == 1);
            GameManager.Instance.GetBackAuthorization();
            progress.StartProgress();
            Assert.IsTrue(GameManager.Instance.GetCurrentAuthorizationPlayer().PlayerID == 2);
            GameManager.Instance.GetBackAuthorization();
        }
    }
}
