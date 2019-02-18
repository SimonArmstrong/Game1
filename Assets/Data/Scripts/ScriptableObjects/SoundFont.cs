using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/SoundFont")]
public class SoundFont : ScriptableObject {
    public AudioClip[] clips;
    public AudioClip GetClip() {
        if (clips.Length <= 0) return null;
        return clips[Random.Range(0, clips.Length)];
    }
}
