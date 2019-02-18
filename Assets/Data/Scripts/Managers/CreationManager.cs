using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public class CreationManager : MonoBehaviour {
    public GENDER gender;
    public string playerName;

    public bool host = true;

    public CharacterData character;

    public GameObject tempCharacterContainer;

    public TinkerAnimator1 baseModel;
    public TinkerAnimator1 hairModel;
    public TinkerAnimator1 eyesModel;
    public TinkerAnimator1 feetModel;
    public TinkerAnimator1 torsoModel;
    public TinkerAnimator1 bottomsModel;

    public TMP_Text networkText;

    public bool UI;

    public GameObject characterController;

    public static CreationManager instance;

    public TMP_InputField nameField;

    public void Left()
    {
        baseModel.Left();
        hairModel.Left();
        eyesModel.Left();
        feetModel.Left();
        torsoModel.Left();
        bottomsModel.Left();
    }

    public void Right()
    {
        baseModel.Right();
        hairModel.Right();
        eyesModel.Right();
        feetModel.Right();
        torsoModel.Right();
        bottomsModel.Right();
    }

    // Use this for initialization
    public void ChangeGender()
    {
        ReloadModel();
    }

    public void LoadModelGear(SerializedItemData idObj, TinkerAnimator1 modelPart)
    {
        ItemDatabase db = GameManager.instance.itemDatabase;

        if (idObj != null)
        {
            if (idObj.itemID != -1)
                modelPart.animations[(int)ANIMATIONS.IDLE] = ((Armour)db.items[idObj.itemID]).idle;
            else
                modelPart.animations[(int)ANIMATIONS.IDLE] = null;
        }
    }

    public void ReloadModel()
    {
        string CHAR_ANIMS = gender == GENDER.FEMALE ? "Character/Animations/Female/" : "Character/Animations/Male/";
        string GEAR_ANIMS = gender == GENDER.FEMALE ? "Gear/Animations/Female/" : "Gear/Animations/Male/";
        string genderPrefix = gender == GENDER.FEMALE ? "F" : "M";

        baseModel.animations[0] = Resources.Load<TAnim>(CHAR_ANIMS + "Base/idle/IDLE_" + genderPrefix);
        //baseModel.animations[1] = Resources.Load<TAnim>(CHAR_ANIMS + "Base/run/RUN_" + genderPrefix);

        LoadModelGear(character.equipment.top, torsoModel);
        LoadModelGear(character.equipment.bottom, bottomsModel);
        LoadModelGear(character.equipment.feet, feetModel);
        LoadModelGear(character.equipment.hair, hairModel);
        LoadModelGear(character.equipment.eyes, eyesModel);
    }

    public void Male()
    {
        gender = GENDER.MALE;
        ChangeGender();
    }

    public void Female()
    {
        gender = GENDER.FEMALE;
        ChangeGender();
    }

    public void FinalizeCreation() {
        playerName = nameField.text;
        character.username = playerName;
        character.gender = gender;
        character.Save();
        //PlayerPrefs.SetString("playerName", playerName);  

        GameObject p = Instantiate(characterController);
        //p.GetComponent<Player>().character = character;

        NetworkManager.singleton.playerPrefab = p;

        //if (host)
        //NetworkManager.singleton.StartHost();
        //else
        //NetworkManager.singleton.StartClient();
    //    CmdSpawnChar(p);

        NetworkServer.Spawn(p);
        SceneManager.LoadScene(1);
        /*
        */
    }
    
    public void ChangeIsHost() {
        host = !host;
        networkText.text = "Host: " + host.ToString() + " | Address: " + NetworkManager.singleton.networkAddress;
    }

    // Use this for initialization
    void Start () {
        //character = new CharacterData(ref character);
        ReloadModel();
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
