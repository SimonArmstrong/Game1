using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName="Tinker Animation", menuName="Tinker/Tinker Animation", order=2)]
public class TinkerAnimation : ScriptableObject {
    public delegate void EventHandle();
    public List<EventHandle> eventHandler = new List<EventHandle>();
    public string mname;
	public Sprite[] sprites;
	public bool loop;

	public float timeBetweenFrames;

    public AudioClip[] sound;
    public float[] motionMultipliers; 
    public bool[] damageCollider;
}
