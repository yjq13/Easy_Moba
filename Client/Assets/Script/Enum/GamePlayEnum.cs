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
        None,
        DamageEffect,
        CureEffect,
        ReliveEffect,
        StaySpElmtPropEffect,
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
        SeekSwordInBoatEffect
    }

    public enum SP_CARD_TYPE
    {
        LAST_CARD_OUT,
    }

    public enum BUFF_TYPE
    {
    	NONE,
    	DRY_WEATHER,
		FIRE_WEAPON,
		FIRE_DESTROY,
		SPARK_1,
		SPARK_2,
		SPARK_3,
		ASH_RELIVE,
		ENDLESS_FIRE,
		DOWN_STREAM,
		WATER_RISE,
		WATER_PRISON,
		WATER_COFFIN,
		SKIP_ROUND,
		STAY_ALIVE,
		AGAINST_STREAM,
		ENDLESS_SPRING,
		INTO_THE_EARTH,
		INTO_THE_EARTH_IGNORE_DAMAGE,
		INTO_THE_EARTH_RM_BUFF,
		GREEDY_FOR_SOLID,
		SOLID_AS_ROCK,
		SOLID_AS_ROCK_CUT_DAMAGE,
		SOLID_AS_ROCK_CUT_SPEED,
		STUBBORN_STONE,
		MUDSLIDE,
		RETREAT_AND_MULTI,
		RETREAT_AND_MULTI_CURE,
		RETREAT_AND_MULTI_RM_BUFF,
		DRY_AND_LUSH_BY_TURNS,
		DRY_AND_LUSH_BY_TURNS_DRAW,
		DRY_AND_LUSH_BY_TURNS_DISCARD,
		DRY_AND_LUSH_BY_TURNS_RM_BUFF,
		PLANT_SOLDIERS,
		WOODEN_COW,
		SILENCE_IS_GOLD,
		SEEK_SWORD_IN_BOAT,
		RETIRE_FROM_BATTLE,
		RETIRE_FROM_BATTLE_IGNORE_DAMAGE,
		RETIRE_FROM_BATTLE_FORBID_GOLD
    }
}
