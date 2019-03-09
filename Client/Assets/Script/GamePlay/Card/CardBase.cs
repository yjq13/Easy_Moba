using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GamePlay
{

    enum CARD_TYPE
    {
        NORMAL = 0,
        WEAPON = 1,
        ACTION = 2
    }

    class CardBase
    {
        public uint CardID;
        public uint CardKeyID;
        public CARD_TYPE CardType = CARD_TYPE.NORMAL;

        public CardBase(uint card_id, uint only_id)
        {
            CardID = card_id;
            CardKeyID = only_id;

            if(card_id == 30 || (card_id >=21 && card_id <=26))
            {
                CardType = CARD_TYPE.WEAPON;
            }
        }
    }
}
