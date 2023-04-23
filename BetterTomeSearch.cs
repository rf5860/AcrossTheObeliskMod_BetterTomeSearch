using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace AcrossTheObeliskMod
{
    [BepInPlugin(PLUGIN_GUID, "Better Tome Search", "1.0.0")]
    public class BetterTomeSearch : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "com.rjf.ato.bettertomesearch";

        public void Awake()
        {
            //This initializes Harmony
            var harmony = new Harmony(PLUGIN_GUID);
            //This applies all patches in your current project
            harmony.PatchAll();
        }
    }

    //This tells Harmony which class you are patching
    [HarmonyPatch(typeof(TomeManager))]
    public class TomeManager_Patch
    {
        const string BetterTomeSearchButton = "BetterTomeSearch";
        static GameObject uiToggle;

        //This tells Harmony that you want to change the "Awake" method of "TomeManager" class.
        [HarmonyPatch(nameof(Awake))]
        //This tells Harmony that you want your code to run after the original code.
        [HarmonyPostfix]
        public static void Awake(TomeManager __instance)
        //Note: all Harmony patch methods must be static even if the original is not.
        {
            uiToggle = CreateToggle();
            uiToggle.transform.position = new Vector3(365f, -80f, 0f);
            uiToggle.transform.SetParent(GameObject.Find("CanvasSearch").transform, false);
            //Add listener for when the state of the Toggle changes, to take action
            Toggle toggleComponent = uiToggle.GetComponent<Toggle>();
            toggleComponent.onValueChanged.AddListener(delegate {
                ToggleValueChanged(toggleComponent);
            });
        }

        // Use reflection to make some of the fields accessible.
        static FieldInfo cardList = typeof(TomeManager).GetField(
            nameof(cardList),
            //Specifiy Public or NonPublic depending on the access modifier of the original method and Instance or Static, or you will get a null reference exception
            BindingFlags.NonPublic | BindingFlags.Instance
        );
        static FieldInfo numCards = typeof(TomeManager).GetField(nameof(numCards), BindingFlags.NonPublic | BindingFlags.Instance);
        static FieldInfo pageOld = typeof(TomeManager).GetField(nameof(pageOld), BindingFlags.NonPublic | BindingFlags.Instance);
        static FieldInfo pageMax = typeof(TomeManager).GetField(nameof(pageMax), BindingFlags.NonPublic | BindingFlags.Instance);
        static FieldInfo pageAct = typeof(TomeManager).GetField(nameof(pageAct), BindingFlags.NonPublic | BindingFlags.Instance);
        static MethodInfo RedoPageNumbers = typeof(TomeManager).GetMethod(nameof(RedoPageNumbers), BindingFlags.NonPublic | BindingFlags.Instance);
        static MethodInfo ActiveDeactiveButtons = typeof(TomeManager).GetMethod(nameof(ActiveDeactiveButtons), BindingFlags.NonPublic | BindingFlags.Instance);

        [HarmonyPatch(nameof(SelectTomeCards))]
        [HarmonyPostfix]
        public static void SelectTomeCards(TomeManager __instance)
        {
            var __cardList = cardList.GetValue(__instance) as List<string>;
            __cardList = __cardList.Where(c => !uiToggle.GetComponent<Toggle>().isOn || Globals.Instance.GetCardData(c, false).CardUpgraded == Enums.CardUpgraded.No).ToList();
            __cardList.Sort();
            cardList.SetValue(__instance, __cardList);
            pageOld.SetValue(__instance, 0);
            pageAct.SetValue(__instance, 0);
            pageMax.SetValue(__instance, Mathf.CeilToInt((float)__cardList.Count / (float)numCards.GetValue(__instance)));
            RedoPageNumbers.Invoke(__instance, null);
        }

        private static void ToggleValueChanged(Toggle change)
        {
            SelectTomeCards(null);
        }

        private static GameObject CreateToggle()
        {
            var checkmarkSprite = Resources.FindObjectsOfTypeAll<Sprite>().First(s => s.name == "Checkmark");
            DefaultControls.Resources uiResources = new DefaultControls.Resources();
            uiResources.checkmark = checkmarkSprite;
            GameObject uiToggle = DefaultControls.CreateToggle(uiResources);
            Text textElement = uiToggle.GetComponentInChildren<Text>();
            textElement.text = "Include Upgraded";
            textElement.fontSize = 16;
            textElement.color = Color.green;
            return uiToggle;
        }
    }
}
