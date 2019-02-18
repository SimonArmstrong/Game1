using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TinkerAnimationMaker : ScriptableWizard
{
    [System.Serializable]
    public struct Sheet {
        public AnimSprite sprite;
        public string name;
    }

    [Header("Item Details")]
    public string itemName;

    [Header("Animation Sheets")]
    public Sheet[] sheets;
    
    //public AnimSprite runSheet;

    [Header("Data Output")]
    [Tooltip("Path extends from Assets folder")]
    public string rootDirectory = "";

    [MenuItem("Animation/From Strip")]
    static void CreateWizard()
    {
        //ScriptableWizard.DisplayWizard<GearCreationWindow>("Create Armour", "Create", "Apply");
        //If you don't want to use the secondary button simply leave it out:

        ScriptableWizard.DisplayWizard<TinkerAnimationMaker>("Create Simple TinkerAnimations", "Create");
    }

    void CreateAnimation(string animName, AnimSprite animSheet)
    {
        TinkerAnimation animation = new TinkerAnimation();

        string finalPath = "Assets/" + rootDirectory + "/" + animName;

        if (!AssetDatabase.IsValidFolder("Assets/" + rootDirectory))
        {
            AssetDatabase.CreateFolder("Assets", rootDirectory);
        }
        if (!AssetDatabase.IsValidFolder("Assets/" + rootDirectory + "/" + animName))
        {
            AssetDatabase.CreateFolder("Assets/" + rootDirectory, animName);
        }

        animation.loop = true;
        animation.timeBetweenFrames = 0.2f;
        animation.sprites = animSheet.sprite;
        
        ScriptableObjectUtility.CreateAsset<TinkerAnimation>(animation, rootDirectory + "/" + animName + "/ANIM_" + animName + "_" + itemName);
    }

    void OnWizardCreate()
    {
        if (itemName == "") return;

        for (int i = 0; i < sheets.Length; i++) {
            CreateAnimation(sheets[i].name, sheets[i].sprite);
        }
    }

    void OnWizardUpdate()
    {
        helpString = "Create simple animations with this Wizard. Can create multiple animations for an item.";
    }

    // When the user presses the "Apply" button OnWizardOtherButton is called.
    void OnWizardOtherButton()
    {
        if (Selection.activeTransform != null)
        {
            Light lt = Selection.activeTransform.GetComponent<Light>();

            if (lt != null)
            {
                lt.color = Color.red;
            }
        }
    }
}