using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {
    public SoundFont soundFont;

    private AudioSource audioSource;

    public void Awake() {
        audioSource.clip = soundFont.GetClip();
    }
}
