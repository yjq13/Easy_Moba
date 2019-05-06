using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class GainAssetEffect : EffectBase
    {
        private ASSET_SUB_TYPE      assetSubType;
        private int                 assetCnt;

        protected override void OnInitEffect(params object[] objs)
        {
            assetSubType    = (ASSET_SUB_TYPE)Enum.Parse(typeof(ASSET_SUB_TYPE), objs[0].ToString());
            assetCnt        = StringConverter.ToInt(objs[1].ToString(), 0);
        }

        protected override void OnTakeEffect(GamePlayer source_player,GamePlayer player)
        {
            if (assetCnt > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                player.Role.GainAsset(assetSubType, assetCnt);
            }
        }
    }
}
