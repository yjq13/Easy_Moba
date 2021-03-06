﻿using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using GameEditor;

namespace GameEditor
{
    public class EffectTestDriverTemplates : TemplateBase
    {
        //param 0: Controller Class name
        //Param 1: View Class name
        public override string GetTemplate()
        {
            return @"using Common;
using GamePlay;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Test
{

    class Test_{0}_Driver : Test_Effect_Driver
    {
        public override void OnEffectTest()
        {
            throw new System.NotImplementedException();
        }
        public override void InitPlayers()
        {
            CardSet cardSet_playerOne = new CardSet();
            List<uint> card_id_list_one = new List<uint> { 1 };
            cardSet_playerOne.InitCardSet(card_id_list_one);
            RoleMage mage = new RoleMage(cardSet_playerOne);
            test_players = new List<GamePlayer>() { new GamePlayer(mage, 1) };
        }
    }
}";
        }

        public override void ShowGetParam()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Effect Name");
            if (template_params == null)
            {
                template_params = new string[1];
                template_params[0] = "NewEffect";
            }
            template_params[0] = EditorGUILayout.TextField(template_params[0]);
            EditorGUILayout.EndHorizontal();
        }

        public override string GetDefaultPath()
        {
            return "Assets/Test/Driver/Game/Effect";
        }

        public override string GetDefaultFileName()
        {
            if(template_params == null)
            {
                return "Test_NewEffect_Driver";
            }
            return "Test_" + template_params[0] + "_Driver";
        }
    }
}
