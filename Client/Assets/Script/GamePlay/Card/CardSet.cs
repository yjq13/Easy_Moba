using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GamePlay
{
    class CardSet
    {
        private List<CardBase> CardSet_Rec;

        public List<CardBase> CarSet_Play;

        public List<CardBase> CardSet_Used;

        public Dictionary<uint, uint> card_record;
        public const uint CardSetCountMax = 30;

        public int RemainingCount
        {
            get
            {
                if(CarSet_Play != null)
                {
                    return CarSet_Play.Count;
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
        }


        public void Reset()
        {
            if(CardSet_Rec != null)
            {
                CarSet_Play = new List<CardBase>(CardSet_Used);
            }
        }

        public CardBase DrawCardRandom()
        {
            if(CarSet_Play == null)
            {
                return null;
            }
            if(CarSet_Play.Count == 0)
            {
                Reset();
            }
            int index = UnityEngine.Random.Range(0, CarSet_Play.Count);
            CardBase card = CarSet_Play[index];
            if(card != null)
            {
                CarSet_Play.RemoveAt(index);
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
    }
}
