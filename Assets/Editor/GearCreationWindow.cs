using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class AnimSprite {
    public Sprite[] sprite;
    public int frames;
}

public class GearCreationWindow : ScriptableWizard {
    [System.Serializable]
    public struct Sheet {
        public AnimSprite sprite;
        public string name;
    }

    [Header("Item Details")]
    public string itemName;
    public GENDER gender;

    [Header("Animation Sheets")]
    public Sheet[] sheets;

    [Header("Database")]
    public ItemDatabase database;
    public bool overrideDBLocation = false;
    public int databaseSlot = 0;
    //public AnimSprite runSheet;

    [Header("Data Output")] [Tooltip("Path extends from Assets folder")]
    public string rootDirectory = "Resources/Gear/Animations/";

    [MenuItem("Add Item/Gear Creation Wizard")]
    static void CreateWizard()
    {
        //ScriptableWizard.DisplayWizard<GearCreationWindow>("Create Armour", "Create", "Apply");
        //If you don't want to use the secondary button simply leave it out:

        ScriptableWizard.DisplayWizard<GearCreationWindow>("Create Armour", "Create");
    }

    void CreateAnimation(ref TAnim anim, string animName, AnimSprite animSheet) {
        TinkerAnimation[] directionAnimations = new TinkerAnimation[4] { new TinkerAnimation(), new TinkerAnimation(), new TinkerAnimation(), new TinkerAnimation() };
        string[] dirs = new string[4] { "FORWARD", "RIGHT", "BACK", "LEFT" };
        string gender_prefix = gender == GENDER.MALE ? "M" : "F";
        string gender_name = gender == GENDER.MALE ? "Male" : "Female";

        string constPath = rootDirectory + gender_name;

        string initPath = constPath + "/" + itemName + " " + gender_prefix + "/" + animName + "/" + itemName + "_" + animName.ToUpper();

        if (!AssetDatabase.IsValidFolder("Assets/" + constPath + "/" + itemName + " " + gender_prefix))
        {
            AssetDatabase.CreateFolder("Assets/" + constPath, itemName + " " + gender_prefix);
        }

        if (!AssetDatabase.IsValidFolder("Assets/" + constPath + "/" + itemName + " " + gender_prefix + "/" + animName))
        {
            AssetDatabase.CreateFolder("Assets/" + constPath + "/" + itemName + " " + gender_prefix, animName);
        }

        anim.anims = new TinkerAnimation[4];
        // Creates animation file for each facing direction
        for (int i = 0; i < directionAnimations.Length; i++)
        {
            directionAnimations[i].loop = true;
            directionAnimations[i].timeBetweenFrames = 0.2f;

            directionAnimations[i].sprites = new Sprite[animSheet.frames];

            // Loop through all our sprites
            for (int a = 0; a < animSheet.frames; a++) {
                directionAnimations[i].sprites[a] = animSheet.sprite[i + (a * 4)];
            }

            ScriptableObjectUtility.CreateAsset<TinkerAnimation>(directionAnimations[i], initPath + "ANIM_" + gender_prefix + "_" + dirs[i]);
            //anim.anims[i] = (TinkerAnimation)Resources.Load(initPath + "ANIM_"+ gender_prefix + "_" + dirs[i]);
            anim.anims[i] = directionAnimations[i];
        }

        ScriptableObjectUtility.CreateAsset<TAnim>(anim, initPath + "_" + gender_prefix);
        AssetDatabase.Refresh();
    }
    TAnim[] _anim;
    void OnWizardCreate()
    {
        if (itemName == "") return;

        Armour armour = new Armour();
        _anim = new TAnim[sheets.Length];

        for (int i = 0; i < sheets.Length; i++)
        {
            CreateAnimation(ref _anim[i], sheets[i].name, sheets[i].sprite);
        }

        armour.idle = GetAnim("Idle");
        armour.run = GetAnim("Run");

        ScriptableObjectUtility.CreateAsset<Armour>(armour, "Resources/Gear/Data/" + itemName + " Data");

        AddToDatabase(armour);
    }

    TAnim GetAnim(string n) {
        TAnim a = null;
        for (int i = 0; i < sheets.Length; i++) {
            if (sheets[i].name == n) {
                a = _anim[i];
            }
        }
        return a;
    }

    void AddToDatabase(Armour a) {
        if (database == null) return;

        if (overrideDBLocation) {
            database.data[databaseSlot] = a;
            return;
        }

        bool replaced = false;
        for (int i = 0; i < database.data.Count; i++)
        {
            if (database.data[i].name == a.name)
            {
                database.data[i] = a;
                replaced = true;
                break;
            }
        }

        if (!replaced) database.data.Add(a);
    }

    void OnWizardUpdate()
    {
        helpString = "Create gear by assigning a name, and the respective animation sprite sheets. All necessary data files will be generated into their respective folders.";
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
