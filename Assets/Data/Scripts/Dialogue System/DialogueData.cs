using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character DialogueData", menuName = "Character Dialogue Data")]
public class DialogueData : ScriptableObject {

    public string idName;

    public string displayName;
    public Sprite sprite;
}
