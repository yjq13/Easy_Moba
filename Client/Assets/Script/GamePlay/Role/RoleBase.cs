using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Common;

namespace GamePlay
{
    public enum RoleType
    {
        ARCHER = 1,
        MEGE = 2,
    }

    public class RoleBase
    {
        private CardSet m_GameCardSet;
        private RoleData m_roleData;
        public List<CardBase> CurrentCanUseCardList;
        public List<CardBase> CurrentUsingCardList;

        public ulong PlayerID { get; private set; }
        public RoleType Role_Type;
        public uint CurrentHP { get; private set; }
        public int ActionPoint  { get; private set;  }
        public float Speed { get; private set; }
        public uint HP_Limit_Max = 100;


        protected RoleBase(CardSet game_card_list, RoleType type)
        {
            m_GameCardSet = game_card_list;
            CurrentUsingCardList = new List<CardBase>();
            CurrentCanUseCardList = new List<CardBase>();
            Role_Type = type;
            int role_type = (int)type;
            m_roleData = ConfigDataManager.Instance.GetData<RoleData>(role_type.ToString());
            CurrentHP = m_roleData.HP;
            Speed = m_roleData.Speed;
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

        public virtual void OnGetCard(uint cardCount)
        {
            
        }

        public void ChangeHP(uint change_hp)
        {
            OnChangeHP(change_hp);
            uint new_hp = CurrentHP + change_hp;
            if(new_hp > HP_Limit_Max)
            {
                CurrentHP = HP_Limit_Max;
            }
            else
            {
                CurrentHP = new_hp;
            }
        }

        public virtual void OnChangeHP(uint change_hp)
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

        public virtual void OnUseCard(CardBase card)
        {
            
        }

        public int GetRemainingCardCount()
        {
            return m_GameCardSet.RemainingCount;
        }

        public void ChangeActionPoint(int change_point)
        {
            ActionPoint += change_point;
        }
    }
}
