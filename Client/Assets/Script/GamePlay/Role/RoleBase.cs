using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Common;
using System.Reflection;

namespace GamePlay
{
    public enum RoleType
    {
        ARCHER = 1,
        MEGE = 2,
    }

    public struct Asset
    {
        public ASSET_MAJOR_TYPE    majorType { get; private set; }
        public ASSET_SUB_TYPE      subType { get; private set; }
        public int                 cnt { get; private set; }
        
        public Asset(ASSET_MAJOR_TYPE major_type, ASSET_SUB_TYPE sub_type, int count)
        {
            majorType   = major_type;
            subType     = sub_type;
            cnt         = count;
        }
    }
    public struct SwordSink
    {
        public int   cardID { get; private set; }
        public int   targetID { get; private set; }
        public float actProgress { get; private set; }

        public SwordSink(int card_id, int target_id, float act_progress)
        {
            cardID      = card_id;
            targetID    = target_id;
            actProgress = act_progress;
        }
    }

    public class RoleBase
    {
        protected CardSet m_GameCardSet;
        protected RoleData m_roleData;

        public List<CardBase> CurrentCanUseCardList;
        public List<CardBase> CurrentUsingCardList;

        public PlayerID PlayerID { get; set; }
        public RoleType Role_Type;
        public int CurrentHP { get; set; }
        public int CurrentActionPoint  { get; set;  }
        public float CurrentSpeed { get; set; }
        public uint HP_Limit_Max = 100;

        // 不走表，在RoleData中无对应的一些属性
        public bool             IsStaySpElmtProp    { get; set; }
        public ELEMENT_PROPERTY StaySpElmtProp      { get; set; }
        public int              SkipCardOutTimes    { get; set; }
        public int              SkipRoundTimes      { get; set; }
        public SwordSink        SwordSink           { get; set; }

        protected RoleBase(CardSet game_card_list, RoleType type)
        {
            m_GameCardSet = game_card_list;
            CurrentUsingCardList = new List<CardBase>();
            CurrentCanUseCardList = new List<CardBase>();
            Role_Type = type;
            int role_type = (int)type;
            m_roleData = ConfigDataManager.Instance.GetData<RoleData>(role_type.ToString());
            FieldInfo[] Properties = m_roleData.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in Properties)
            {
                PropertyInfo targetProperty = GetType().GetProperty("Current" + property.Name);
                object value = property.GetValue(m_roleData);
                if (targetProperty != null && value != null)
                {
                    targetProperty.SetValue(this, value,null);
                }
            }
            //CurrentHP = m_roleData.HP;
            //CurrentSpeed = m_roleData.Speed;

            IsStaySpElmtProp    = false;
            StaySpElmtProp      = ELEMENT_PROPERTY.NONE;
            SkipCardOutTimes    = 0;
            SkipRoundTimes      = 0; 
        }

        public RoleType GetRoleType()
        {
            return Role_Type;
        }

        //？？？ 弃牌实现
        public void DiscardCard(CardBase card)
        {
            OnDiscardCard(card);
            if (card != null)
            {
                CurrentCanUseCardList.Remove(card);
            }
        }

        protected void OnDiscardCard(CardBase card)
        {

        }

        public void GetCard(int cardCount)
        {
            OnGetCard(cardCount);
            if(m_GameCardSet != null)
            {
                for(int i = 0; i < cardCount; i++)
                {
                    CardBase card_get = m_GameCardSet.DrawCardRandom();
                    CurrentCanUseCardList.Add(card_get);
                }
            }
        }

        protected virtual void OnGetCard(int cardCount)
        {
            
        }

        public void ChangeHP(int change_hp)
        {
            OnChangeHP(change_hp);
            int new_hp = CurrentHP + change_hp;
            if(new_hp > HP_Limit_Max)
            {
                CurrentHP = (int)HP_Limit_Max;
            }
            else
            {
                CurrentHP = new_hp;
            }
        }

        protected virtual void OnChangeHP(int change_hp)
        {

        }

        public void UseCard(CardBase card)
        {
            OnUseCard(card);
            if(card != null)
            {
                CurrentCanUseCardList.Remove(card);
                if (card.CardType == CARD_TYPE.WEAPON)
                {
                    CurrentUsingCardList.Add(card);
                }
                else
                {
                    m_GameCardSet.UseCard(card);
                }
            }
        }

        protected virtual void OnUseCard(CardBase card)
        {
            
        }

        public int GetRemainingCardCount()
        {
            return m_GameCardSet.RemainingCount;
        }

        public void ChangeActionPoint(int change_point)
        {
            CurrentActionPoint += change_point;
        }

        // ========================================================-
        public void ChangeStaySpElmtProp(ELEMENT_PROPERTY elmt_prop)
        {
            IsStaySpElmtProp = true;
            StaySpElmtProp   = elmt_prop;
            OnChangeStaySpElmtProp(elmt_prop);
        }

        protected virtual void OnChangeStaySpElmtProp(ELEMENT_PROPERTY elmt_prop)
        {

        }

        // ---------------------------------------------------------
        public void ChangeSpeedCnt(int speed_cnt)
        {
            CurrentSpeed += speed_cnt;
            OnChangeSpeedCnt(speed_cnt);
        }

        protected virtual void OnChangeSpeedCnt(int speed_cnt)
        {

        }

        // ---------------------------------------------------------
        public void ChangeSpeedPct(int speed_pct)
        {
            CurrentSpeed += (CurrentSpeed * speed_pct / 100);
            OnChangeSpeedPct(speed_pct);
        }

        protected virtual void OnChangeSpeedPct(int speed_pct)
        {

        }
        // ---------------------------------------------------------
        public void AddSkipCardOutTimes(int add_times)
        {
            SkipCardOutTimes += add_times;
            OnAddSkipCardOutTimes(add_times);
        }

        protected virtual void OnAddSkipCardOutTimes(int add_times)
        {

        }

        // ---------------------------------------------------------
        public void AddSkipRoundTimes(int add_times)
        {
            SkipRoundTimes += add_times;
            OnAddSkipRoundTimes(add_times);
        }

        protected virtual void OnAddSkipRoundTimes(int add_times)
        {

        }

        // ---------------------------------------------------------
        public void GainSpCard(int card_id, int card_cnt)
        {
            // !!! 暂未实现
            OnGainSpCard(card_id, card_cnt);
        }

        protected virtual void OnGainSpCard(int card_id, int card_cnt)
        {

        }

        // ---------------------------------------------------------
        // 资源数量
        public uint GetAssetCnt(ASSET_MAJOR_TYPE mj_type, ASSET_SUB_TYPE sub_type)
        {

            return 0;
        }

        public uint GetAssetCnt(ASSET_SUB_TYPE sub_type)
        {
            // !!! 暂未实现
            return 0;
        }

        // ---------------------------------------------------------
        // 最后一次获取的资源
        public Asset LastAcquiredAsset()
        {
            // !!! 暂未实现
            Asset asset = new Asset(ASSET_MAJOR_TYPE.NONE, ASSET_SUB_TYPE.NONE, 0);
            return asset;
        }

        // ---------------------------------------------------------
        public void GainAsset(ASSET_MAJOR_TYPE mj_type, ASSET_SUB_TYPE sub_type, int asset_cnt)
        {
            // !!! 暂未实现
            OnGainAsset(mj_type, sub_type, asset_cnt);
        }

        protected virtual void OnGainAsset(ASSET_MAJOR_TYPE mj_type, ASSET_SUB_TYPE sub_type, int asset_cnt)
        {

        }

        // ---------------------------------------------------------
        public void GainAsset(ASSET_SUB_TYPE sub_type, int asset_cnt)
        {
            // !!! 暂未实现
            OnGainAsset(sub_type, asset_cnt);
        }

        protected virtual void OnGainAsset(ASSET_SUB_TYPE sub_type, int asset_cnt)
        {

        }

        // ---------------------------------------------------------
        public void TransAssetTo(GamePlayer source_player, ASSET_MAJOR_TYPE mj_type, ASSET_SUB_TYPE sub_type, uint asset_cnt)
        {
            // !!! 暂未实现
            OnTransAssetTo(source_player,mj_type, sub_type, asset_cnt);
        }

        protected virtual void OnTransAssetTo(GamePlayer source_player, ASSET_MAJOR_TYPE mj_type, ASSET_SUB_TYPE sub_type, uint asset_cnt)
        {

        }

        // ---------------------------------------------------------
        public void StoleAsset(GamePlayer target_player, ASSET_SUB_TYPE sub_type, uint asset_cnt)
        {
            // !!! 暂未实现
            OnStoleAsset(target_player, sub_type, asset_cnt);
        }

        protected virtual void OnStoleAsset(GamePlayer target_player, ASSET_SUB_TYPE sub_type, uint asset_cnt)
        {

        }

        // ---------------------------------------------------------//SZY Error 重复命名 改名SetSwordSink
        public void SetSwordSink(int card_id, int target_uid, float act_progress)
        {
            SwordSink = new SwordSink(card_id, target_uid, act_progress);
            OnSwordSink(card_id, target_uid, act_progress);
        }

        protected virtual void OnSwordSink(int card_id, int target_uid, float act_progress)
        {

        }

        // ---------------------------------------------------------
        public int GetLastCardID()
        {
            // !!! 暂未实现
            return 0;
        }

        // ---------------------------------------------------------
        // 进度条百分比
        public float GetActProgress()
        {
            if (GameFacade.GetCurrentCardGame() != null)
            {
                return GameFacade.GetCurrentCardGame().GetSpeedProgress().GetProgressPercentByPlayer(PlayerID);
            }
            else
            {
                Debug.LogError("Cant find the CurrentGame");
            }
            return 0;
        }

        // 更改这个角色的当前进度
        public void ChangeActProgress(float percent)
        {
            if (GameFacade.GetCurrentCardGame() != null)
            {
                GameFacade.GetCurrentCardGame().GetSpeedProgress().SetProgressPercentByPlayer(PlayerID,percent);
            }
            else
            {
                Debug.LogError("Cant find the CurrentGame");
            }
        }
    }
}
