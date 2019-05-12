using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class CutJustAcquiredAssetEffect : EffectBase
    {
        private ASSET_MAJOR_TYPE    assetMjType;
        private ASSET_SUB_TYPE      assetSubType;

        protected override void OnInitEffect(params object[] objs)
        {
            assetMjType     = (ASSET_MAJOR_TYPE)Enum.Parse(typeof(ASSET_MAJOR_TYPE), objs[0].ToString());
            assetSubType    = (ASSET_SUB_TYPE)Enum.Parse(typeof(ASSET_SUB_TYPE), objs[0].ToString());
        }

        protected override void OnTakeEffect(GamePlayer source_player,GamePlayer player)
        {
            Asset lastAcqAsset = player.Role.LastAcquiredAsset();
            if (lastAcqAsset.majorType == assetMjType && lastAcqAsset.subType == assetSubType && lastAcqAsset.cnt > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                player.Role.GainAsset(assetMjType, assetSubType, lastAcqAsset.cnt);
            }
        }
    }
}
