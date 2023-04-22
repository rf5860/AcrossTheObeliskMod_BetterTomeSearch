using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using System.Linq.Expressions;

namespace AcrossTheObeliskMod2
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class BetterTomeSearch : BaseUnityPlugin
    {
        public const string pluginGuid = "com.rjf.ato.bettertomesearch";
        public const string pluginName = "Better Tome Search";
        public const string pluginVersion = "1.0.0";

        public void Awake()
        {
            Logger.LogInfo("Better Tome Search Mod Loaded!");
        }
    }

    [HarmonyPatch(typeof(TomeManager))]
    public class TomeManager_Patch
    {

        [HarmonyPatch(nameof(Refresh))]
        [HarmonyPostfix]
        public static void Refresh(TomeManager __instance)
        {
            AccessTools.FieldRefAccess<CardCraftManager>();
            AccessTools.FieldRefAccess<Transform, CardCraftItem>(__instance, "quickStartButton");
        }
    }
}
