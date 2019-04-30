using Common;
using GamePlay;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Test
{
    class Test_DamageEffect_Driver : DriverBase
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
            List<uint> card_id_list_one = new List<uint> { 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 14, 19, 27, 29, 8, 2, 2, 2, 2, 2, 2, 7, 7, 7, 9, 9, 9, 23, 26, 11 };
            cardSet_playerOne.InitCardSet(card_id_list_one);

            RoleMage mage = new RoleMage(cardSet_playerOne);
            GamePlayer player_one = new GamePlayer(mage, 1);

            EffectInfoData effect_info = new EffectInfoData();
            EffectBase effect = EffectFactory.CreateEffect(effect_info);
            List<GamePlayer> targets = null;
            GameTargetManager.Instance.StartGetTarget(player_one, effect_info.TargetType);
            targets = GameTargetManager.Instance.GetChoosedTarget();
            if (targets != null)
            {
                effect.TakeEffect(targets, effect_info.EffectParam1, effect_info.EffectParam2);
            }
        }
    }
}