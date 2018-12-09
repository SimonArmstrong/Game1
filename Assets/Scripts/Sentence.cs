using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sentence {

    [Tooltip("Set to true if this is the first sentence in the dialogue or the first sentence from a new perspective.")]
    public bool prevSentenceClear = true;

    [TextArea(3, 10)]
    public string sentence;
}
