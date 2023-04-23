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
        static FieldInfo searchTerm = typeof(TomeManager).GetField(nameof(searchTerm), BindingFlags.NonPublic | BindingFlags.Instance);
        static FieldInfo activeTomeCards = typeof(TomeManager).GetField(nameof(activeTomeCards), BindingFlags.NonPublic | BindingFlags.Instance);
        static MethodInfo RedoPageNumbers = typeof(TomeManager).GetMethod(nameof(RedoPageNumbers), BindingFlags.NonPublic | BindingFlags.Instance);
        static MethodInfo ActivateDeactivateButtons = typeof(TomeManager).GetMethod(nameof(ActivateDeactivateButtons), BindingFlags.NonPublic | BindingFlags.Instance);
        static MethodInfo SetPage = typeof(TomeManager).GetMethod(nameof(SetPage), BindingFlags.Public | BindingFlags.Instance);

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

        static void ToggleValueChanged(Toggle change)
        {
            TomeManager.Instance.SelectTomeCards((int)activeTomeCards.GetValue(TomeManager.Instance), true);
        }

        //This tells Harmony that you want to change the "DoTomeCards" method of "TomeManager" class.
        [HarmonyPatch(nameof(DoTomeCards))]
        //This tells Harmony that you want your code to run after the original code.
        [HarmonyPostfix]
        public static void DoTomeCards(TomeManager __instance)
        //Note: all Harmony patch methods must be static even if the original is not.
        {
            if (GameObject.Find("CanvasSearch").GetComponentInChildren<Toggle>() == null)
            {
                uiToggle = CreateToggle();
                uiToggle.transform.position = new Vector3(365f, -80f, 0f);
                uiToggle.transform.SetParent(GameObject.Find("CanvasSearch").transform, false);
                //Add listener for when the state of the Toggle changes, to take action
                Toggle toggleComponent = uiToggle.GetComponent<Toggle>();
                toggleComponent.onValueChanged.AddListener(delegate { ToggleValueChanged(toggleComponent); });
            }
        }

        [HarmonyPatch(nameof(SelectTomeCards))]
        [HarmonyPrefix]
        public static bool SelectTomeCards(ref TomeManager __instance, int index = -1, bool absolute = false)
        {
            var __activeTomeCards = (int)activeTomeCards.GetValue(__instance);
            var __cardList = cardList.GetValue(__instance) as List<string>;
            var __searchTerm = searchTerm.GetValue(__instance) as string;
            if (index == __activeTomeCards && !absolute) return false;
            activeTomeCards.SetValue(__instance, index);
            List<string> list = new List<string>();
            if (index == -1)
            {
                list = Globals.Instance.CardListNotUpgraded;
            }
            else if (index == 0)
            {
                list = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Warrior];
            }
            else if (index == 1)
            {
                list = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Mage];
            }
            else if (index == 2)
            {
                list = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Healer];
            }
            else if (index == 3)
            {
                list = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Scout];
            }
            else if (index == 4 && GameManager.Instance.GetDeveloperMode())
            {
                list = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Monster];
            }
            else if (index == 5)
            {
                list = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Boon];
            }
            else if (index == 6)
            {
                list = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Injury];
            }
            else if (index == 7)
            {
                list = Globals.Instance.CardItemByType[Enums.CardType.Weapon];
            }
            else if (index == 8)
            {
                list = Globals.Instance.CardItemByType[Enums.CardType.Armor];
            }
            else if (index == 9)
            {
                list = Globals.Instance.CardItemByType[Enums.CardType.Jewelry];
            }
            else if (index == 10)
            {
                list = Globals.Instance.CardItemByType[Enums.CardType.Accesory];
            }
            else if (index == 11)
            {
                list = Globals.Instance.CardItemByType[Enums.CardType.Pet];
            }
            else if (index == 22)
            {
                list = Globals.Instance.CardListByType[Enums.CardType.Enchantment];
            }
            __cardList.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                CardData cardData = Globals.Instance.GetCardData(list[i], false);
                if (!(cardData != null) || cardData.ShowInTome)
                {
                    if (__searchTerm.Trim() != "")
                    {
                        var includeUpgraded = GameObject.Find("CanvasSearch").GetComponentInChildren<Toggle>() == null || GameObject.Find("CanvasSearch").GetComponentInChildren<Toggle>().GetComponent<Toggle>().isOn;
                        if (index != 22 || cardData.CardClass != Enums.CardClass.Monster)
                        {
                            if (Globals.Instance.IsInSearch(__searchTerm, list[i]) && (includeUpgraded || cardData.CardUpgraded == Enums.CardUpgraded.No))
                            {
                                __cardList.Add(list[i]);
                            }
                            if (cardData != null && index != 22 && includeUpgraded)
                            {
                                if (cardData.UpgradesTo1 != "" && Globals.Instance.IsInSearch(__searchTerm, cardData.UpgradesTo1))
                                {
                                    __cardList.Add(cardData.UpgradesTo1);
                                }
                                if (cardData.UpgradesTo2 != "" && Globals.Instance.IsInSearch(__searchTerm, cardData.UpgradesTo2))
                                {
                                    __cardList.Add(cardData.UpgradesTo2);
                                }
                                if (cardData.UpgradesToRare != null && Globals.Instance.IsInSearch(__searchTerm, cardData.UpgradesToRare.Id))
                                {
                                    __cardList.Add(cardData.UpgradesToRare.Id);
                                }
                            }
                        }
                    }
                    else if (index != 22 || (cardData.CardUpgraded == Enums.CardUpgraded.No && cardData.CardClass != Enums.CardClass.Monster))
                    {
                        __cardList.Add(list[i]);
                    }
                }
            }
            __cardList.Sort();
            pageOld.SetValue(__instance, 0);
            pageAct.SetValue(__instance, 0);
            pageMax.SetValue(__instance, Mathf.CeilToInt(__cardList.Count / (int)numCards.GetValue(__instance)));
            RedoPageNumbers.Invoke(__instance, null);
            ActivateDeactivateButtons.Invoke(__instance, new object[] { index });
            SetPage.Invoke(__instance, new object[] { 1, true });

            return false;
        }

    }
}
