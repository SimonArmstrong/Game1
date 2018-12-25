using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerSound : ScriptableObject {
    public string idName;
    public AudioClip[] clips;
    public AudioClip GetSound(int i = (-1)) {
        if (i == -1)
            i = Random.Range(0, clips.Length);

        if (i > clips.Length - 1) {
            i = clips.Length - 1;
        }

        if (i < 0) {
            i = 0;
        }

        return clips[i];
    }
}
