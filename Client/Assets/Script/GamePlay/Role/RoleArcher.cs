using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Common;

namespace GamePlay
{
    public class RoleArcher : RoleBase
    {
        private CardSet m_GameCardSet;
        private RoleData m_roleData;


        public RoleArcher(CardSet game_card_list):base(game_card_list, RoleType.ARCHER)
        {
            
        }
    }
}
