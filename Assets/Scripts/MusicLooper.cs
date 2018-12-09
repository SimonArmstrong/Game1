using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLooper : MonoBehaviour {
    [System.Serializable]
    public struct Section {
        public float startTime;
        public float endTime;
        public bool loop;
        public bool ProcessSection(float t) {
            bool val = false;
            if (t >= startTime && t <= endTime) {
                val = true;
            }
            return val;
        }
    }

    public List<Section> section = new List<Section>();

    private AudioSource audioSource;
    public int currentSection;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if (section[currentSection].loop && audioSource.time == section[currentSection].endTime) {
            audioSource.time = section[currentSection].startTime;
        }

        if (!section[currentSection].loop && audioSource.time == section[currentSection].endTime) {
            currentSection++;
        }

    }
}
