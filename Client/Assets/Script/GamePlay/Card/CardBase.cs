using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public CARD_TYPE CardType;

        public CardBase(uint card_id, uint only_id)
        {
            CardID = card_id;
            CardKeyID = CardKeyID;
        }
    }
}
