using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase")]
public class ItemDatabase : ScriptableObject {
    public List<Item> data;
}
