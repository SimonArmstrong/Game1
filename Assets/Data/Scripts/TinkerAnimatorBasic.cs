﻿using UnityEngine;
using System.Collections;

public class TinkerAnimatorBasic : MonoBehaviour {
	public TinkerAnimation[] animations;
	public int currentAnimation = 0;
	public bool destroyAfterPlayed;
	public SpriteRenderer renderer;

	public int frameIndex = 0;
	float timer;

	public bool done;

	private bool isChild = false;
    SpriteRenderer sr;

    private void Start(){
		
	}

    private void OnValidate()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) sr = gameObject.AddComponent<SpriteRenderer>();
        if(animations.Length > 0 && animations[0].sprites.Length > 0)
            sr.sprite = animations[0].sprites[0];
    }

    private void SwitchFrames (){
		// Reset frames if we've reached the end -- Creates Loop
		if (frameIndex > animations [currentAnimation].sprites.Length - 1) {
			if (destroyAfterPlayed)
				Destroy (gameObject);
			if (animations [currentAnimation].loop) {
				frameIndex = 0;
				done = false;
			} else {
				frameIndex = 0;
				done = true;
			}
		}

		// If we haven't involved a renderer, involove it
		if (renderer == null) {
			renderer = GetComponent<SpriteRenderer> ();
		}

        if (animations[currentAnimation].sound.Length >= animations[currentAnimation].sprites.Length) {
            if (animations[currentAnimation].sound[frameIndex] != null) {
                CreateSound(animations[currentAnimation].sound[frameIndex]);
            }
        }

		// 
		renderer.sprite = animations[currentAnimation].sprites [frameIndex];
	}

	private void Update(){
		if (animations.Length > 0) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				frameIndex++;
				SwitchFrames ();
				timer = animations [currentAnimation].timeBetweenFrames;
			}
		}
	}

	public TinkerAnimation GetCurrentAnimation(){
		return animations [currentAnimation];
	}

    public void CreateSound(AudioClip clip)
    {
        AudioSource a = Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        a.clip = clip;
        a.volume = 0.1f;
    }

    public string GetAnimationName(){
		return animations [currentAnimation].name;
	}
}
