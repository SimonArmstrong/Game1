using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TAnim")]
public class TAnim : ScriptableObject {
    public TinkerAnimation[] anims;
    public bool hasSound = false;
}
