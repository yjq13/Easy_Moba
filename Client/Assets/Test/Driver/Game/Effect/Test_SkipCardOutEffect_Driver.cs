using Common;
using GamePlay;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Test
{

    class Test_SkipCardOutEffect_Driver : Test_Effect_Driver
    {
        public override void OnEffectTest()
        {
            throw new System.NotImplementedException();
        }
        public override void InitPlayers()
        {
            CardSet cardSet_playerOne = new CardSet();
            List<uint> card_id_list_one = new List<uint> { 1 };
            cardSet_playerOne.InitCardSet(card_id_list_one);
            RoleMage mage = new RoleMage(cardSet_playerOne);
            test_players = new List<GamePlayer>() { new GamePlayer(mage, 1) };
        }
    }
}