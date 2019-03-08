using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlay
{
    class Player
    {
        private CardSet m_GameCardSet;
        public List<CardBase> CurrentCanUseCardList;
        public List<CardBase> CurrentUsingCardList;

        public int CurrentHP { get; set; }
        public int HP_Limit_Max = 100;

        public Player(CardSet game_card_list)
        {
            m_GameCardSet = game_card_list;
            CurrentHP = 100;
        }

        public void GetCard(uint cardCount)
        {
            if(m_GameCardSet != null)
            {
                for(int i = 0; i < cardCount; i++)
                {
                    CardBase card_get = m_GameCardSet.DrawCardRandom();
                    CurrentCanUseCardList.Add(card_get);
                }
            }
        }

        public void ChangeHP(int change_hp)
        {
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

        public void UseCard(CardBase card)
        {
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
    }
}
