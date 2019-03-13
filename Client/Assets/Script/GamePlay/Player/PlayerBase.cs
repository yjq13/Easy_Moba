using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GamePlay
{
    public class PlayerBase
    {
        private CardSet m_GameCardSet;
        public List<CardBase> CurrentCanUseCardList;
        public List<CardBase> CurrentUsingCardList;

        public ulong PlayerID { get; private set; }
        public int CurrentHP { get; private set; }
        public int ActionPoint  { get; private set;  }
        public int HP_Limit_Max = 100;

        public PlayerBase(CardSet game_card_list,RoleData role_config_data)
        {
            m_GameCardSet = game_card_list;
            CurrentUsingCardList = new List<CardBase>();
            CurrentCanUseCardList = new List<CardBase>();
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

        public void ChangeHP(int change_hp)
        {
            OnChangeHP(change_hp);
            int new_hp = CurrentHP + change_hp;
            if(new_hp > HP_Limit_Max)
            {
                CurrentHP = HP_Limit_Max;
            }
            else
            {
                CurrentHP = new_hp;
            }
        }

        public virtual void OnChangeHP(int change_hp)
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
