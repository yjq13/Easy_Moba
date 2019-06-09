using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class StoleAssetIfNotHaveEffect : EffectBase
    {
        private ASSET_SUB_TYPE      assetSubType;
        private int                 assetCnt;

        protected override void OnInitEffect(params object[] objs)
        {
            assetSubType    = (ASSET_SUB_TYPE)Enum.Parse(typeof(ASSET_SUB_TYPE), objs[0].ToString());
            assetCnt        = Convert.ToInt32(objs[1]);
        }

        protected override void OnTakeEffect(GamePlayer source_player, GamePlayer target_player)
        {
            int HaveCnt = (int)source_player.Role.GetAssetCnt(assetSubType);
            if (HaveCnt < assetCnt)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                source_player.Role.StoleAsset(target_player, assetSubType, (uint)assetCnt);
            }
        }
    }
}
