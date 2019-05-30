using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    public enum ELEMENT_PROPERTY
    {
        NONE        = 0,
        FIRE        = 1,
        WATER       = 2,
        SOLID       = 3,
        WOOD        = 4,
        GOLD        = 5,
    }

    public enum ASSET_MAJOR_TYPE
    {
        NONE,
        MAGIC_POINT
    }

    public enum ASSET_SUB_TYPE
    {
        NONE,
        FIRE,
        WATER,
        SOLID,
        WOOD,
        GOLD
    }

    public enum EFFECT_TYPE
    {
        DamageEffect,
        CureEffect,
        ReliveEffect,
        StaySpElementPropertyEffect,
        ChangeSpeedCntEffect,
        ChangeSpeedPercentageEffect,
        SkipCardOutEffect,
        SkipRoundEffect,
        StayAliveEffect,
        RedirectToSpCardEffect,
        ReplayLastCardCurRoundEffect,
        DrawCardEffect,
        GainSpCardEffect,
        DiscardCardEffect,
        IgnoreDamageEffect,
        GainBuffEffect,
        ClearBuffLeftTimesEffect,
        IgnoreDamageBuffEffect,
        IgnoreDamageDebuffEffect,
        StoleRandomBuffEffect,
        CutJustAcquiredAssetEffect,
        TransferJustAcquiredAssetEffect,
        MultiAssetGainEffect,
        GainAssetEffect,
        ClearAssetEffect,
        StoleAssetIfNotHaveEffect,
        SwordSinkEffect,
        seekSwordInBoatEffect
    }

    public enum SP_CARD_TYPE
    {
        LAST_CARD_OUT,
    }

    public abstract class EffectBase
    {
        private ELEMENT_PROPERTY elementProp;

        public  ELEMENT_PROPERTY EltProp
        {
            get
            {
                return elementProp;
            }
        }

        // 必须传入元素属性。元素属性=卡牌属性或buff属性
        public void InitEffect(ELEMENT_PROPERTY property, params object[] objs)
        {
            elementProp = (ELEMENT_PROPERTY)Enum.Parse(typeof(ELEMENT_PROPERTY), objs[0].ToString());
            OnInitEffect(objs);
        }

        public void TakeEffect(GamePlayer source_player, List<GamePlayer> players, params object[] objs)
        {
            if(players == null)
            {
                return;
            }
            foreach(var player in players)
            {
                OnTakeEffect(source_player, player);
            }

        }

        protected abstract void OnInitEffect(params object[] objs);

        protected abstract void OnTakeEffect(GamePlayer source_player, GamePlayer player);
    }
}
