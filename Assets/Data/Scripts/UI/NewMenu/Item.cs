using UnityEngine;
using UnityEditor;
using System.Text;

public enum _Element {
    NULL,
    WATER = 100,
    FIRE = 200,
    THUNDER = 300,
    EARTH = 400,
    VOID = 500,
    LIGHT = 600
}

[CreateAssetMenu(menuName="Items/Item")]
public class Item : ScriptableObject {

    public _Element element;
    
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string databaseID;
    public string ItemName;
    public TinkerAnimation iconAnim;
    public float value;
    [Range(1,999)]
    public int MaximumStacks = 1;
    
    protected static readonly StringBuilder sb = new StringBuilder();

    public virtual void OnValidate() {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual Item GetCopy(){
        return this;
    }

    public virtual void Destroy() { 
        
    }

    public virtual string GetItemType() {
        return "";
    }

    public virtual string GetDescription() {
        return "";
    }
}

