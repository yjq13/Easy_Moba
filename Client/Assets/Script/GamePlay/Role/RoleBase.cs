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

    public class RoleBase
    {
        protected CardSet m_GameCardSet;
        protected RoleData m_roleData;

        public List<CardBase> CurrentCanUseCardList;
        public List<CardBase> CurrentUsingCardList;

        public ulong PlayerID { get; private set; }
        public RoleType Role_Type;
        public int CurrentHP { get; private set; }
        public int CurrentActionPoint  { get; private set;  }
        public float CurrentSpeed { get; private set; }
        public uint HP_Limit_Max = 100;

        // 不走表，在RoleData中无对应的一些属性
        public bool             IsStaySpElmtProp    { get; private set; }
        public ELEMENT_PROPERTY StaySpElmtProp      { get; private set; }
        public int              SkipCardOutTimes    { get; private set; }
        public int              SkipRoundTimes      { get; private set; }
        //public ValueTuple<int, int, float> SwordSink     { get; private set; } 、//SZY Error 目前不支持元组这个东西啊……unity只支持C#4.0以前的特性

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
                    Debug.Log(targetProperty.Name);
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

        public void GetCard(uint cardCount)
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

        protected virtual void OnGetCard(uint cardCount)
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
                Debug.LogError("UseCard:"+card.CardID);
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
            OnChangeSpElmtProp(elmt_prop);
        }

        protected virtual void OnChangeStaySpElmtProp(ELEMENT_PROPERTY elmt_prop)
        {

        }

        // ---------------------------------------------------------
        public void ChangeSpeedCnt(int speed_cnt)
        {
            CurrentSpeed += change_speed;
            OnChangeSpeedCnt(change_speed);
        }

        protected virtual void OnChangeSpeedCnt(int speed_cnt)
        {

        }

        // ---------------------------------------------------------
        public void ChangeSpeedPct(int speed_pct)
        {
            CurrentSpeed += (CurrentSpeed * speed_pct / 100);
            OnChangeSpeedPct(change_speed);
        }

        protected virtual void OnChangeSpeedPct(int speed_pct)
        {

        }
        // ---------------------------------------------------------
        public void AddSkipCardOutTimes(bool add_times)
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
        public void GainSpCard(int card_id, uint card_cnt)
        {
            // !!! 暂未实现
            OnGainSpCard(card_id, card_cnt);
        }

        protected virtual void OnGainSpCard(int card_id, uint card_cnt)
        {

        }

        // ---------------------------------------------------------
        // 资源数量
        public uint GetAssetCnt(ASSET_MAJOR_TYPE mj_type, ASSET_SUB_TYPE sub_type)
        {
            // !!! 暂未实现
            return 0;
        }

        public uint GetAssetCnt(ASSET_SUB_TYPE sub_type)
        {
            // !!! 暂未实现
            return 0;
        }

        // ---------------------------------------------------------
        // 最后一次获取的资源
        public uint LastAcquiredAsset()
        {
            // !!! 暂未实现
            return new Tuple<ASSET_MAJOR_TYPE, ASSET_SUB_TYPE, uint>(ASSET_MAJOR_TYPE.NONE, ASSET_SUB_TYPE.NONE, 0);
        }

        // ---------------------------------------------------------
        public void GainAsset(ASSET_MAJOR_TYPE mj_type, ASSET_SUB_TYPE sub_type, uint asset_cnt)
        {
            // !!! 暂未实现
            OnGainAsset(mj_type, sub_type, asset_cnt);
        }

        protected virtual void OnGainAsset(ASSET_MAJOR_TYPE mj_type, ASSET_SUB_TYPE sub_type, uint asset_cnt)
        {

        }

        // ---------------------------------------------------------
        public void GainAsset(ASSET_SUB_TYPE sub_type, uint asset_cnt)
        {
            // !!! 暂未实现
            OnGainAsset(sub_type, asset_cnt);
        }

        protected virtual void OnGainAsset(ASSET_SUB_TYPE sub_type, uint asset_cnt)
        {

        }

        // ---------------------------------------------------------
        public void TransAssetTo(GamePlayer source_player, ASSET_MAJOR_TYPE mj_type, ASSET_SUB_TYPE sub_type, uint asset_cnt)
        {
            // !!! 暂未实现
            OnTransAssetTo(mj_type, sub_type, asset_cnt);
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

        // ---------------------------------------------------------//SZY Error 重复命名
        //public void SwordSink(int card_id, int target_uid, float act_progress)
        //{
        //    SwordSink = new Tuple<int, int, float>(card_id, target_uid, act_progress);
        //    OnSwordSink(target_player, sub_type, asset_cnt);
        //}

        protected virtual void OnSwordSink(GamePlayer target_player, ASSET_SUB_TYPE sub_type, uint asset_cnt)
        {

        }

        // ---------------------------------------------------------
        public int GetLastCardID()
        {
            // !!! 暂未实现
            return 0;
        }

    }
}
