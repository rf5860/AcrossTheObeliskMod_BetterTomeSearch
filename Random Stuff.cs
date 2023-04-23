using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Linq;

namespace AcrossTheObeliskMod2
{
    //[BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    //public class BetterTomeSearch : BaseUnityPlugin
    //{
    //    public const string pluginGuid = "com.rjf.ato.bettertomesearch";
    //    public const string pluginName = "Better Tome Search";
    //    public const string pluginVersion = "1.0.0";

    //    public void Awake()
    //    {
    //        Logger.LogInfo("Better Tome Search Mod Loaded!");
    //    }
    //}



    //public class Example : MonoBehaviour
    //{
    //    public Texture2D texture;
    //    public GUIContent content = new GUIContent();
    //    public string label = "ON/OFF";
    //    public Rect checkBoxRect;
    //    public Rect labelRect;
    //    public Rect labelRectX;
    //    public bool allOptions = true;
    //    public GUIStyle labelStyle;
    //    public GUIStyle labelStyleX;
        
    //    void Awake()
    //    {
    //        labelStyle = new GUIStyle();
    //        labelStyle.fontSize = 32;
    //        labelStyle.normal.textColor = Color.red;
    //        labelStyle.fontStyle = FontStyle.Bold;
    //        checkBoxRect = new Rect(0, 0, 64, 64);
    //        labelRect = new Rect(68, 16, 128, 32);
    //        labelStyle.fontSize = 0;
    //        labelRectX = new Rect(8, 0, 128, 32);
    //        labelStyleX = new GUIStyle();
    //        labelStyleX.fontSize = 64;
    //        labelStyleX.normal.textColor = Color.green;
    //        labelStyleX.fontStyle = FontStyle.Bold;
    //    }

    //    void Update()
    //    {
    //        if (allOptions)
    //            content.text = "X";
    //        else
    //            content.text = "0";
    //    }

    //    void OnGUI()
    //    {
    //        GUI.Label(labelRect, label, labelStyle);
    //        GUI.Label(labelRectX, "X", labelStyleX);

    //        if (GUI.Button(checkBoxRect, content.image))
    //        {
    //            allOptions = !allOptions;
    //        }
    //    }
    //}


    //[HarmonyPatch(typeof(TomeManager))]
    //public class TomeManager_Patch
    //{
    //    //Example button name
    //    const string BetterTomeSearchButton = "BetterTomeSearch";

    //    [HarmonyPatch(nameof(Refresh))]
    //    [HarmonyPostfix]
    //    public static void Refresh(TomeManager __instance)
    //    {
    //        // Get a reference to the existing "Upgraded" filter button used in card crafting in town.
    //        var a = AtOManager.Instance.cardcratPrefab;
    //        var advancedCraft = CardCraftManager.Instance.buttonAdvancedCraft;
    //        var advancedCraftPosition = advancedCraft.transform.position;
    //        var advancedCraftRotation = advancedCraft.transform.rotation;
    //        Component[] advancedCraftComponents = advancedCraft.GetComponents(typeof(Component));
    //        /*
    //         * 4 - BotonAdvancedCraft           - This is advancedCraft
    //         * 3 - UnityEngine.Animator         - 
    //         * 2 - BotonGeneric                 - Unsure
    //         * 1 - UnityEngine.BoxCollider2D    - Controls behaviour around hovering over and clicking the button
    //         * 0 - UnityEngine.Transform
    //         */

    //        // This Controls behaviour around hovering over and clicking the button
    //        var boxCollider = advancedCraft.GetComponent<BoxCollider2D>();
    //        // ???
    //        var genericButton = advancedCraft.GetComponent<BotonGeneric>();
    //        var transform = advancedCraft.GetComponent<Transform>();
    //        // 
    //        var animator = advancedCraft.GetComponent<Animator>();





    //        //var advancedCraft = CardCraftManager.Instance.buttonAdvancedCraft;
    //        // var genericButton = advancedCraft.GetComponent<BotonGeneric>();
    //        var affordableCraft = CardCraftManager.Instance.buttonAffordableCraft;
    //        var genericAffordableButton = affordableCraft.GetComponent<BotonGeneric>();

    //        // Background used when the option is enabled (But not the button)
    //        var backgroundOverAdvanced = genericButton.backgroundOver;
    //        var backgroundOverAffordable = genericAffordableButton.backgroundOver;
    //        // backgroundOverAdvanced.gameObject.SetActiveRecursively(true)

    //        // Background used when the option is disabled (But not the button)
    //        var backgroundDisabledAdvanced = genericButton.backgroundDisable;
    //        var backgroundDisabledAffordable = genericAffordableButton.backgroundDisable;

    //        // Responsible for glowing border
    //        // Appears disabled by default (Might be used for when you get something new?)
    //        var borderAdvanced = genericButton.border;
    //        var borderAffordable = genericAffordableButton.border;


    //        var spriteRendererBorderAdvanced = borderAdvanced.GetComponent<SpriteRenderer>();
    //        var spriteRendererBorderAffordable = borderAffordable.GetComponent<SpriteRenderer>();
    //        var spriteRendererDisabledAdvanced = backgroundDisabledAdvanced.GetComponent<SpriteRenderer>();
    //        var spriteRendererDisabledAffordable = backgroundDisabledAffordable.GetComponent<SpriteRenderer>();
    //        var spriteRendererOverAdvanced = backgroundOverAdvanced.GetComponent<SpriteRenderer>();
    //        var spriteRendererOverAffordable = backgroundOverAffordable.GetComponent<SpriteRenderer>();

    //        var backupSprite = spriteRendererDisabledAffordable.sprite;
    //        spriteRendererDisabledAffordable.sprite = spriteRendererDisabledAdvanced.sprite;
    //        var backupSprite2 = spriteRendererOverAffordable.sprite;
    //        spriteRendererOverAffordable.sprite = spriteRendererOverAdvanced.sprite;



    //        //spriteRendererOverAffordable.sprite.texture
    //        //spriteRendererOverAdvanced.sprite.texture

    //        var cardCraft = UnityEngine.Object.Instantiate<GameObject>(AtOManager.Instance.cardcratPrefab, new Vector3(0f, 0f, -2f), Quaternion.identity);
    //        var craftManager = cardCraft.GetComponent<CardCraftManager>();
    //        var cardSection = AccessTools.FieldRefAccess<TomeManager, Transform>(TomeManager.Instance, "cardsSection");
    //        var cardSectionContainer = cardSection.parent.gameObject;
    //        // advancedButton.transform.parent = cardSectionContainer.transform;
    //        //Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.tomeTs[indexTomeCard]);
    //        //TomeManager.Instance.tomeTs[0].parent.gameObject.GetComponents<Component>()[0]
    //        //var cardSection = AccessTools.FieldRefAccess<TomeManager, Transform>(TomeManager.Instance, "cardsSection");
    //        var cardContainer = AccessTools.FieldRefAccess<TomeManager, Transform>(TomeManager.Instance, "cardContainer");
    //        var craftManagerPrefab = AtOManager.Instance.cardcratPrefab.GetComponent<CardCraftManager>();

    //        //TomeManager.Instance.tomeTs
    //        var advancedButton1 = UnityEngine.Object.Instantiate<BotonAdvancedCraft>(
    //            craftManagerPrefab.buttonAdvancedCraft,
    //            new Vector3(6.93f, 4.9f, 1f),
    //            Quaternion.identity,
    //            TomeManager.Instance.transform.GetChild(0)
    //         );
    //        var advancedButton2 = UnityEngine.Object.Instantiate<BotonAdvancedCraft>(
    //            craftManagerPrefab.buttonAdvancedCraft,
    //            new Vector3(6.93f, 4.9f, 1f),
    //            Quaternion.identity,
    //            TomeManager.Instance.transform.GetChild(0)
    //         );
    //        advancedButton1.transform.SetParent(GameObject.Find("CanvasSearch").transform, false);
    //        advancedButton2.transform.SetParent(GameObject.Find("CanvasSearch").transform, true);
    //        //transform.GetChild(0).gameObject.GetComponents<Component>()[0]
    //        var t = advancedButton1.gameObject.transform;
    //        var newButton = new GameObject("BetterTomeSearch") { };
    //        newButton.AddComponent<Example>();
    //        newButton.transform.SetParent(GameObject.Find("CanvasSearch").transform); // CRITICAL
    //        var c = GameObject.Find("CanvasSearch").transform.GetChild(0); // Get the <X> button
    //        var i = c.transform.GetChild(0).gameObject.GetComponent<Image>(); // Get image
    //        var ii = i.transform.GetChild(0); // Component
    //        var text = ii.gameObject.GetComponent<TextMeshProUGUI>();
    //        var example = newButton.GetComponent<Example>();
    //        example.te
    //        //var checkmark = example.gameObject.AddComponent<TextMeshProUGUI>();
    //        //checkmark.text = "X";
    //        var labelStyle = new GUIStyle();
    //        labelStyle.fontSize = 32;
    //        labelStyle.normal.textColor = Color.green;
    //        labelStyle.fontStyle = FontStyle.Bold;
    //        GUI.Label(new Rect(0, 0, 64, 64), "X", labelStyle);
    //        // WRONG - b.gameObject.GetComponent<TextMeshProUGUI>()
    //        //https://forum.unity.com/threads/colliders-dont-work-as-expected.462310/ 
    //        //https://stackoverflow.com/questions/69427848/how-to-modify-ui-text-via-script

    //        //SceneManager.MoveGameObjectToScene(advancedButton.gameObject, SceneManager.GetSceneByName("DontDestroyOnLoad"));
    //        ////SceneManager.MoveGameObjectToScene(advancedButton.gameObject, SceneManager.GetSceneByName("TomeOfKnowledge"));
    //        /////SceneManager.MoveGameObjectToScene(advancedButton.gameObject, SceneManager.GetSceneByName(SceneStatic.GetSceneName()));
    //        //// https://stackoverflow.com/questions/45916417/how-to-add-gameobject-in-canvas-without-any-changes
    //        //// Transform newButton = (Transform)Instantiate (Button, new Vector3(0,0,0), Quaternion.identity);
    //        //// newButton.localScale = new Vector3(1, 1, 1);
    //        //// newButton.SetParent(GameObject.Find("Canvas").transform, false);
    //        //advancedButton.transform.SetParent(GameObject.Find("CanvasSearch").transform, true);
    //        var checkmarkSprite = Resources.FindObjectsOfTypeAll<Sprite>().First(s => s.name == "Checkmark");
    //        DefaultControls.Resources uiResources = new DefaultControls.Resources();
    //        uiResources.checkmark = checkmarkSprite;
    //        GameObject uiToggle = DefaultControls.CreateToggle(uiResources);
    //        Text textElement = uiToggle.GetComponentInChildren<Text>();
    //        textElement.text = "Include Upgraded";
    //        textElement.fontSize = 16;
    //        textElement.color = Color.green;
    //        uiToggle.transform.position = new Vector3(365f, -80f, 0f);
    //        uiToggle.transform.SetParent(GameObject.Find("CanvasSearch").transform, false);












    //        var mapLayer2 = SortingLayer.layers[2];
    //        var defaultLayer6 = SortingLayer.layers[6];
    //        var charLayer1 = SortingLayer.layers[1];

    //        SortingLayer.layers[2] = defaultLayer6;
    //        SortingLayer.layers[6] = mapLayer2;
    //        SortingLayer.layers[6] = defaultLayer6;
    //        SortingLayer.layers[2] = mapLayer2;

    //        SortingLayer.layers[1] = defaultLayer6;
    //        SortingLayer.layers[6] = charLayer1;





    //        var betterTomeSearch = new GameObject("BetterTomeSearch") { };
    //        var newAdvancedCraft = betterTomeSearch.AddComponent<BotonAdvancedCraft>();
    //        var newAnimator = betterTomeSearch.AddComponent<Animator>();
    //        // This one shits the bed
    //        var newGenericButton = betterTomeSearch.AddComponent<BotonGeneric>();
    //        var newBox = betterTomeSearch.AddComponent<BoxCollider2D>();
    //        var newTransform = betterTomeSearch.AddComponent<Transform>();



    //        var betterTomeSR = betterTomeSearch.AddComponent<SpriteRenderer>();
















    //        var bg = CardCraftManager.Instance.buttonAdvancedCraft.GetComponent<BotonGeneric>();
    //        var bgt = bg.transform;
    //        var np = new Vector3(4.98f, -4.7f, -4.0f);
    //        var bgp = bgt.position; // Invoked REPL, result: (4.98, -4.70, -4.00)
    //        var nr = new Quaternion(0.00000f, 0.00000f, 0.00000f, 1.00000f);
    //        var bgr = bgt.rotation; // Invoked REPL, result: (0.00000, 0.00000, 0.00000, 1.00000)
    //        bgp.x = 100;
    //        bgt.SetPositionAndRotation(bgp, bgr);

    //        // 5 components
    //        Component[] cs = bg.GetComponents(typeof(Component));
    //        /*
    //         * 4 - BotonAdvancedCraft
    //         * 3 - UnityEngine.Animator
    //         * 2 - BotonGeneric
    //         * 1 - UnityEngine.BoxCollider2D
    //         * 0 - UnityEngine.Transform
    //         */
    //        newButton = new GameObject("BetterTomeSearch") { };
    //        newButton.AddComponent<BotonGeneric>();
    //        var nb = newButton; var nbt = nb.transform;
    //        nbt.SetPositionAndRotation(np, nr);

    //        // var cardSection = AccessTools.FieldRefAccess<TomeManager, Transform>(__instance, "cardsSection");
    //        var cardSection = AccessTools.FieldRefAccess<TomeManager, Transform>(TomeManager.Instance, "cardsSection");
    //        var cardSectionContainer = cardSection.parent.gameObject;

    //        // nbt.parent = container.transform;
    //        // nbt.parent = cardSection;
    //        nbt.parent = TomeManager.Instance.transform;

    //        var upgradedFilterButton = AccessTools.FieldRefAccess<CardCraftManager, BotonAdvancedCraft>(CardCraftManager.Instance, "buttonAdvancedCraft");
    //        // Get a reference to card search section and its parent container in the Tome of Knowledge.
    //        // Only add the filter if it doesn't already exist
    //        var existingButton = container.transform.Find(BetterTomeSearchButton)?.gameObject;
    //        if (existingButton == null)
    //        {
    //            //Clone the base button (note, other buttons and controls will need different properties cloned).
    //            myButton_go = CloneButton(myButtonName, quickStartButton.gameObject);
    //            //Add a click handler to your button.
    //            myButton_go.GetComponent<UIButton>().LeftClick += Button_LeftClick;
    //            //Attach your button to the button container.
    //            myButton_go.transform.parent = buttonContainer.transform;
    //        }
    //    }

    //    private static void ClickHandler(IUIButton obj)
    //    {
    //        var message = new MessageModalWindow.Message
    //        {
    //            Title = "Test",
    //            Description = "My new button works!",
    //            Buttons = new[]
    //            {
    //                new MessageBoxButton.Data(MessageBox.Choice.Ok, null, isDismiss: true)
    //            }
    //        };
    //        MessageModalWindow.ShowMessage(message);
    //    }

    //    private static GameObject CloneButton(string newName, GameObject original)
    //    {
    //        var newButton = new GameObject(newName) { };
    //        newButton.AddComponent<BotonGeneric>();

    //        //Add other components and style them like the original button

    //        //Setup blur
    //        var originalBlur = original.GetComponent<BlurBackgroundWidget>();
    //        var blur = newButton.AddComponent<BlurBackgroundWidget>();
    //        blur.BottomLeftCornerRadius = originalBlur.BottomLeftCornerRadius;
    //        blur.BottomRightCornerRadius = originalBlur.BottomRightCornerRadius;
    //        blur.TopLeftCornerRadius = originalBlur.TopLeftCornerRadius;
    //        blur.TopRightCornerRadius = originalBlur.TopRightCornerRadius;
    //        blur.Color = originalBlur.Color;
    //        blur.Gradient = originalBlur.Gradient;
    //        blur.BlurGradient = originalBlur.Gradient;

    //        //Setup visuals
    //        var originalSquircle = original.GetComponent<SquircleBackgroundWidget>();
    //        var squircle = newButton.AddComponent<SquircleBackgroundWidget>();
    //        squircle.BackgroundColor = originalSquircle.BackgroundColor;
    //        squircle.OuterBorderColor = originalSquircle.OuterBorderColor;
    //        squircle.BorderColor = originalSquircle.BorderColor;
    //        squircle.BorderGradient = originalSquircle.BorderGradient;
    //        squircle.CornerRadius = originalSquircle.CornerRadius;

    //        squircle.BackgroundGradient = originalSquircle.BackgroundGradient;
    //        squircle.HighlightColor = originalSquircle.HighlightColor;
    //        squircle.HighlightGradient = originalSquircle.HighlightGradient;

    //        //Setup text
    //        var originalLabel = original.GetComponent<UILabel>();
    //        var label = newButton.AddComponent<UILabel>();
    //        label.Text = newName;
    //        label.Alignment = originalLabel.Alignment;
    //        label.FontFamily = originalLabel.FontFamily;
    //        label.Color = originalLabel.Color;
    //        label.FontSize = originalLabel.FontSize;
    //        label.ForceCaps = originalLabel.ForceCaps;
    //        label.Margins = originalLabel.Margins;
    //        label.InterLetterAdditionalSpacing = originalLabel.InterLetterAdditionalSpacing;
    //        label.InterLineAdditionalSpacing = originalLabel.InterLineAdditionalSpacing;
    //        label.InterParagraphAdditionalSpacing = originalLabel.InterParagraphAdditionalSpacing;
    //        label.UseAscent = originalLabel.UseAscent;
    //        label.WordWrap = originalLabel.WordWrap;

    //        //Setup tooltip
    //        var originalTooltip = original.GetComponent<UITooltip>();
    //        var tooltip = newButton.AddComponent<UITooltip>();
    //        tooltip.Message = "Tooltip works!";
    //        tooltip.AnchorMode = originalTooltip.AnchorMode;

    //        //Added automatically when other amplitude ui components went in. Adjust dimensions to match original.
    //        var originalTransform = original.GetComponent<UITransform>();
    //        var uitransform = newButton.GetComponent<UITransform>();
    //        uitransform.Width = originalTransform.Width;
    //        uitransform.Height = originalTransform.Height;

    //        return newButton;
    //    }
    //}
}
