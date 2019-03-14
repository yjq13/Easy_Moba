
using System.Collections.Generic;
using Common;
using GamePlay;
using UnityEditor;
using UnityEngine;

namespace Test
{
    class CardPlayTestLancher
    {
        static RoleBase player_one;
        static RoleBase player_two;
        static bool show_card_one;
        static bool show_card_two;
        static int hp_1 = 0;
        static int hp_2 = 0;
        static int action_1 = 0;
        static int action_2 = 0;
        static int Random = 0;
        static bool result = false;
        [MenuItem("Tools/Test for play")]
        public static void TestForPlay()
        {
            CardSet cardSet_playerOne = new CardSet();
            CardSet cardSet_playerTwo = new CardSet();
            List<uint> card_id_list_one = new List<uint> { 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 14, 19, 27, 29, 8, 2, 2, 2, 2, 2, 2, 7, 7, 7, 9, 9, 9, 23, 26, 11 };
            List<uint> card_id_list_tow = new List<uint> { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 2, 2, 2, 2, 2, 2, 6, 18, 18, 18, 8, 8, 8, 20, 7, 7, 7, 24, 26, 12 };
            cardSet_playerOne.InitCardSet(card_id_list_one);
            cardSet_playerTwo.InitCardSet(card_id_list_tow);
            player_one = new RoleBase(cardSet_playerOne, RoleType.ARCHER);
            player_two = new RoleBase(cardSet_playerTwo, RoleType.ARCHER);


            TestWindow myWindow = (TestWindow)EditorWindow.GetWindow(typeof(TestWindow), false, "MyWindow", false);
            myWindow.Show(true);
        }

        public class TestWindow : EditorWindow
        {
            void OnGUI()
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("抽卡:player1"))
                {
                    player_one.GetCard(1);
                }
                if (GUILayout.Button("显示/隐藏"))
                {
                    show_card_one = !show_card_one;
                }
                hp_1 = EditorGUILayout.IntField(hp_1);
                if (GUILayout.Button("+-HP"))
                {
                    player_one.ChangeHP(hp_1);
                }
                action_1 = EditorGUILayout.IntField(action_1);
                if (GUILayout.Button("+-action"))
                {
                    player_one.ChangeActionPoint(action_1);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("HP:"+player_one.CurrentHP);
                GUILayout.Label("Action:" + player_one.ActionPoint);
                GUILayout.Label("RemainingCount:" + player_one.GetRemainingCardCount());
                string usingCard = "";
                foreach(var card in player_one.CurrentUsingCardList)
                {
                    usingCard += card.CardID.ToString()+"  ";
                }
                GUILayout.Label("UsingCard:"+usingCard);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if(show_card_one)
                {
                    foreach (var card in player_one.CurrentCanUseCardList)
                    {
                        if (GUILayout.Button(card.CardID.ToString()))
                        {
                            player_one.UseCard(card);
                        }
                    }
                }

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("抽卡:player2"))
                {
                    player_two.GetCard(1);
                }
                if (GUILayout.Button("显示/隐藏"))
                {
                    show_card_two = !show_card_two;
                }

                hp_2 = EditorGUILayout.IntField(hp_2);
                if (GUILayout.Button("+-HP"))
                {
                    player_two.ChangeHP(hp_2);
                }
                action_2 = EditorGUILayout.IntField(action_2);
                if (GUILayout.Button("+-action"))
                {
                    player_two.ChangeActionPoint(action_2);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("HP:" + player_two.CurrentHP);
                GUILayout.Label("Action:" + player_two.ActionPoint);
                GUILayout.Label("RemainingCount:" + player_two.GetRemainingCardCount());
                string usingCard1 = "";
                foreach (var card in player_two.CurrentUsingCardList)
                {
                    usingCard1 += card.CardID.ToString() + "  ";
                }
                GUILayout.Label("UsingCard:" + usingCard1);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (show_card_two)
                {
                    foreach (var card in player_two.CurrentCanUseCardList)
                    {
                        if (GUILayout.Button(card.CardID.ToString()))
                        {
                            player_two.UseCard(card);
                        }
                    }
                }
                GUILayout.EndHorizontal();
                Random = EditorGUILayout.IntField(Random);
                if (GUILayout.Button("是否命中"))
                {
                    result = UnityEngine.Random.Range(0, 100) <= Random;
                }
                GUILayout.Label("result:" + result);
            }
            void OnInspectorUpdate()
            {
                if (EditorWindow.mouseOverWindow)
                    EditorWindow.mouseOverWindow.Focus();//就是当鼠标移到那个窗口，这个窗口就自动聚焦
                this.Repaint();//重画MyWindow窗口，更新Label
            }
        }
    }


}