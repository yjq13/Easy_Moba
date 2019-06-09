using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

namespace GamePlay
{
    class TransferJustAcquiredAssetEffect : EffectBase
    {
        private ASSET_MAJOR_TYPE    assetMjType;
        private ASSET_SUB_TYPE      assetSubType;

        protected override void OnInitEffect(params object[] objs)
        {
            assetMjType     = (ASSET_MAJOR_TYPE)Enum.Parse(typeof(ASSET_MAJOR_TYPE), objs[0].ToString());
            assetSubType    = (ASSET_SUB_TYPE)Enum.Parse(typeof(ASSET_SUB_TYPE), objs[1].ToString());
        }

        protected override void OnTakeEffect(GamePlayer source_player, GamePlayer player)
        {
            // !!!
            // 暂未实现
            // 需要提供一个接口 拿到某玩家刚刚（本次）（即将）获得的资源
            uint assetCnt = player.Role.GetAssetCnt(assetMjType, assetSubType);
            if (assetCnt > 0)
            {
                // player.SendGameBuffTriggerEvent( Buff_NOTIFY_TYPE.GET_DAMAGE, this);
                player.Role.GainAsset(assetMjType, assetSubType, 0 - (int)assetCnt);
            }
        }
    }
}
