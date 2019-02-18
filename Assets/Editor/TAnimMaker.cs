using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TAnimMaker : ScriptableWizard
{
    [System.Serializable]
    public struct Sheet {
        public AnimSprite sprite;
        public string name;
    }


    [Header("Item Details")]
    public string itemName;
    public string groupName;

    [Header("Animation Sheets")]
    public Sheet[] sheets;
    
    //public AnimSprite runSheet;

    [Header("Data Output")]
    [Tooltip("Path extends from Assets folder")]
    public string rootDirectory = "Animations";

    [MenuItem("Animation/From Matrix")]
    static void CreateWizard()
    {
        //ScriptableWizard.DisplayWizard<GearCreationWindow>("Create Armour", "Create", "Apply");
        //If you don't want to use the secondary button simply leave it out:

        ScriptableWizard.DisplayWizard<TAnimMaker>("Create 4-Way TAnims", "Create");
    }

    void CreateAnimation(ref TAnim anim, string animName, AnimSprite animSheet)
    {
        TinkerAnimation[] directionAnimations = new TinkerAnimation[4] { new TinkerAnimation(), new TinkerAnimation(), new TinkerAnimation(), new TinkerAnimation() };
        string[] dirs = new string[4] { "FORWARD", "RIGHT", "BACK", "LEFT" };
        string finalPath = "";

        if (groupName.Length > 0)
            finalPath = rootDirectory + "/" + groupName + "/" + animName;
        else
            finalPath = rootDirectory + "/" + animName;


        if (!AssetDatabase.IsValidFolder("Assets/" + rootDirectory))
        {
            AssetDatabase.CreateFolder("Assets", rootDirectory);
        }
        if (groupName.Length > 0)
        {
            if (!AssetDatabase.IsValidFolder("Assets/" + rootDirectory + "/" + groupName))
            {
                AssetDatabase.CreateFolder("Assets/" + rootDirectory, groupName);
            }
            if (!AssetDatabase.IsValidFolder("Assets/" + rootDirectory + "/" + groupName + "/" + animName))
            {
                AssetDatabase.CreateFolder("Assets/" + rootDirectory + "/" + groupName, animName);
            }
        }
        else
        {
            if (!AssetDatabase.IsValidFolder("Assets/" + rootDirectory + "/" + animName))
            {
                AssetDatabase.CreateFolder("Assets/" + rootDirectory, animName);
            }
        }

        anim.anims = new TinkerAnimation[4];
        // Creates animation file for each facing direction
        for (int i = 0; i < directionAnimations.Length; i++)
        {
            directionAnimations[i].loop = animSheet.loop;
            directionAnimations[i].timeBetweenFrames = 0.2f;

            directionAnimations[i].sprites = new Sprite[animSheet.frames];

            // Loop through all our sprites
            for (int a = 0; a < animSheet.frames; a++)
            {
                directionAnimations[i].sprites[a] = animSheet.sprite[i + (a * 4)];
            }
           
            ScriptableObjectUtility.CreateAsset<TinkerAnimation>(directionAnimations[i], finalPath + "/ANIM_" + itemName + "_" + dirs[i]);
            //anim.anims[i] = (TinkerAnimation)Resources.Load(initPath + "ANIM_"+ gender_prefix + "_" + dirs[i]);
            anim.anims[i] = directionAnimations[i];
        }
        ScriptableObjectUtility.CreateAsset<TAnim>(anim, finalPath + "/" + itemName + "_" + animName);
        AssetDatabase.Refresh();
    }

    void OnWizardCreate()
    {
        if (itemName == "") return;

        TAnim[] _anim = new TAnim[sheets.Length];

        for (int i = 0; i < sheets.Length; i++) {
            _anim[i] = new TAnim();
            CreateAnimation(ref _anim[i], sheets[i].name, sheets[i].sprite);
        }
    }

    void OnWizardUpdate()
    {
        helpString = "Create 4-Way directional animations with this Wizard.";
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