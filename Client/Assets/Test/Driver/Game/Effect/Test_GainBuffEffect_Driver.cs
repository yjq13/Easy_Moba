using Common;
using GamePlay;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Test
{

    class Test_GainBuffEffect_Driver : Test_Effect_Driver
    {
        public override void OnEffectTest()
        {
            EffectInfoData effect_info = new EffectInfoData(GameTargetType.OPPO_ONE, EFFECT_TYPE.GainBuffEffect, "DRY_WEATHER", "2");
            EffectBase effect = EffectFactory.CreateEffect(effect_info);
            List<GamePlayer> targets = null;
            targets = test_players;
            if (targets != null)
            {
                effect.TakeEffect(test_players[0], targets, effect_info.EffectParam1, effect_info.EffectParam2);
            }
            // !!!
            // 暂未实现
            Assert.IsTrue(true);
        }
        
        public override void InitPlayers()
        {
            CardSet cardSet_playerOne = new CardSet();
            List<uint> card_id_list_one = new List<uint> { 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 14, 19, 27, 29, 8, 2, 2, 2, 2, 2, 2, 7, 7, 7, 9, 9, 9, 23, 26, 11 };
            cardSet_playerOne.InitCardSet(card_id_list_one);
            RoleMage mage   = new RoleMage(cardSet_playerOne);
            GamePlayer test_player     = new GamePlayer(mage, 1);
            test_players    = new List<GamePlayer>() { test_player };
        }
    }
}