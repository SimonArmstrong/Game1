using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animation Set")]
public class AnimationSet : ScriptableObject {
    public TinkerAnimation[] idles;
    public TinkerAnimation[] runs;
}
