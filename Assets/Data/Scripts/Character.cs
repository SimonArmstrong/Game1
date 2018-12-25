using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;


[System.Serializable]
public class Attributes {
    public float maxHP;
    public float maxMP;
    public float strength;
    public float intelligence;
    public float dexterity;
    public float luck;
    public float moveSpeed;
    public float swimSpeed;

    public Attributes() {
        maxHP = 50;
        maxMP = 50;

        strength = 4;
        intelligence = 4;
        dexterity = 4;
        luck = 4;

        moveSpeed = 2.3f;
        swimSpeed = 1.5f;
    }
    public Attributes(float f) {
        maxHP = f;
        maxMP = f;

        strength = f;
        intelligence = f;
        dexterity = f;
        luck = f;

        moveSpeed = f;
        swimSpeed = f;
    }

    public static Attributes operator* (Attributes a, Attributes b) {
        a.maxHP *= b.maxHP;
        a.maxMP *= b.maxMP;
        a.moveSpeed *= b.moveSpeed;
        a.swimSpeed *= b.moveSpeed;
        a.strength *= b.moveSpeed;
        a.intelligence *= b.intelligence;
        a.dexterity *= b.dexterity;
        a.luck *= b.luck;
        return a;
    }
}

[System.Serializable]
public class Stats {
    public float attack;
    public float defense;
    public float magicAtt;
    public float magicDef;
    public float attackSpeed;
    public float castSpeed;
    public float critChance;
    public float critDamage;

    public Stats() {
        attack = 0;
        defense = 0;
        magicAtt = 0;
        magicDef = 0;
        attackSpeed = 1;
        castSpeed = 1;
        critChance = 0.05f;
        critDamage = 1.5f;
    }
}

[System.Serializable]
public class SerializedItemData {
    public int itemID;
    public SerializedItemData() {
        itemID = -1;
    }
}


[System.Serializable]
public class CharacterData {
    public string username;
    public int id;
    public GENDER gender;

    public Attributes attributes;
    public Stats stats;

    [System.Serializable]
    public class Equipment
    {
        [SerializeField]
        public SerializedItemData top, bottom, feet, gloves, hair, eyes;
    }

    [System.Serializable]
    public class Inventory
    {

        [SerializeField]
        public SerializedItemData[] items;
        public Inventory()
        {
            items = new SerializedItemData[25];
        }
    }

    public Equipment equipment;
    public Inventory inventory;

    public CharacterData() {
        this.username = "";
        this.gender = GENDER.FEMALE;
        attributes = new Attributes();
        stats = new Stats();
        equipment = new Equipment();
        inventory = new Inventory();
        id = 0;
    }

    public CharacterData(string name = "", GENDER gender = GENDER.FEMALE) {
        this.username = name;
        this.gender = gender;
        attributes = new Attributes();
        stats = new Stats();
        equipment = new Equipment();
        inventory = new Inventory();
        id = 0;
    }

    public void Save() {
        XmlSerializer serializer = new XmlSerializer(typeof(CharacterData));
        TextWriter writer = new StreamWriter("SaveData/" + username + "_" + id.ToString("000000") + ".xml");
        serializer.Serialize(writer, this);
        writer.Close();
    }

    public static CharacterData Load(string charID) {
        XmlSerializer serializer = new XmlSerializer(typeof(CharacterData));
        FileStream fs = new FileStream("SaveData/" + charID + ".xml", FileMode.Open);
        CharacterData loaded;
        loaded = (CharacterData)serializer.Deserialize(fs);
        fs.Dispose();
        return loaded;
    }
}