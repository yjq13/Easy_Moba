
//Author：Jiaqi.Ying

//摘要：卡组类，用来管理一个玩家的当前卡组的卡

using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class CardSet
    {
        private List<CardBase> CardSet_Rec;

        public List<CardBase> CardSet_Play;

        public List<CardBase> CardSet_Used;

        public Dictionary<uint, uint> card_record = new Dictionary<uint, uint>();
        public const uint CardSetCountMax = 30;

        public int RemainingCount
        {
            get
            {
                if(CardSet_Play != null)
                {
                    return CardSet_Play.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        public CardSet()
        {
            CardSet_Rec = new List<CardBase>();
            CardSet_Play = new List<CardBase>();
            CardSet_Used = new List<CardBase>();
        }

        //随机抽卡
        public CardBase DrawCardRandom()
        {
            if(CardSet_Play == null)
            {
                return null;
            }
            if(CardSet_Play.Count == 0)
            {
                Reset();
            }
            int index = UnityEngine.Random.Range(0, CardSet_Play.Count);
            CardBase card = CardSet_Play[index];
            if(card != null)
            {
                CardSet_Play.RemoveAt(index);
                return card;
            }
            else
            {
                return null;
            }
        }

        public void UseCard(CardBase card)
        {
            CardSet_Used.Add(card);
        }

        public void InitCardSet(List<uint> cardIDList)
        {
            foreach(uint card_id in cardIDList)
            {
                AddPrepareCard(card_id);
            }
            StartCard();
        }

        public void AddPrepareCard(uint card_id, uint count = 1)
        {
            if (card_id != 0)
            {
                if (CardSet_Rec != null)
                {
                    for (uint index = 0; index < count; index++)
                    {

                        uint count_rec = 0;
                        CardBase new_card = null;
                        if (card_record.TryGetValue(card_id, out count_rec))
                        {
                            count_rec++;
                        }

                        new_card = new CardBase(card_id, count);

                        card_record[card_id] = count;
                        if(CardSet_Rec.Count < CardSetCountMax)
                        {
                            CardSet_Rec.Add(new_card);
                        }
                        else
                        {
                            Debug.LogError("cards is limited : 30");
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void RemovePrepareCard(uint card_id, uint count = 1)
        {

        }

        public void AddCard(CardBase card)
        {

        }

        public void RemoveCard(CardBase card)
        {

        }

        public CardBase FindCardByID(uint id)
        {
            return null;
        }


        private void StartCard()
        {
            if (CardSet_Rec != null)
            {
                foreach (var card in CardSet_Rec)
                {
                    CardSet_Play.Add(card);
                }
            }
        }

 //--------------------------------private method--------------------------//

        private void Reset()
        {
            if (CardSet_Used != null)
            {
                foreach (var card in CardSet_Used)
                {
                    CardSet_Play.Add(card);
                }
                CardSet_Used.Clear();
            }
        }
    }
}
