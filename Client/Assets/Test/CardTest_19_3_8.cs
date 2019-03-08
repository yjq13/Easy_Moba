
using System.Collections.Generic;
using GamePlay;
using UnityEditor;

namespace Test
{
    class CardTest_19_3_8
    {
        [MenuItem("Tools/Check Using Sprites")]
        public static void TestForPlay()
        {
            CardSet cardSet_playerOne = new CardSet();
            CardSet cardSet_playerTwo = new CardSet();

            List<uint> card_id_list_one = new List<uint> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            List<uint> card_id_list_tow = new List<uint> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            cardSet_playerOne.InitCardSet(card_id_list_one);
            cardSet_playerOne.InitCardSet(card_id_list_tow);
        }


    }
}
