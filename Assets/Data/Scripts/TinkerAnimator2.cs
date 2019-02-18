using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TinkerAnimator2 : MonoBehaviour {
	public TinkerAnimation[] animations;
	public int currentAnimation = 0;
	public bool destroyAfterPlayed;
	public Image renderer;
	public Camera cam;
	public bool player;
    //public int dir;

	int frameIndex = 0;
	float timer;

	public bool done;

	private bool isChild = false;

	private void Start(){
		
	}
    

	private void SwitchFrames (){
        if (animations.Length < 1) return;

        if (animations[0] == null)
        {
            renderer.sprite = null;
            return;
        }

        if (currentAnimation >= animations.Length - 1)
        {
            currentAnimation = animations.Length - 1;
        }

        if (frameIndex > animations[currentAnimation].sprites.Length - 1)
        {
            if (destroyAfterPlayed)
                Destroy(gameObject);
            if (animations[currentAnimation].loop)
            {
                frameIndex = 0;
                done = false;
            }
            else
            {
                frameIndex = 0;
                done = true;
            }
        }


        // 
        renderer.sprite = animations[currentAnimation].sprites[frameIndex];
    }

	private void Update(){
        // If we haven't involved a renderer, involove it
        if (renderer == null)
        {
            renderer = GetComponent<Image>();
        }
        if (animations.Length <= 0) {
            renderer.sprite = null;
            renderer.color = new Color(0, 0, 0, 0);
            return;
        }
        if (animations[currentAnimation] == null) {
            return;
        }
        //renderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        if (animations.Length > 0) {
			timer -= Time.unscaledDeltaTime;
			if (timer <= 0) {
				frameIndex++;
				SwitchFrames ();
				timer = animations [currentAnimation].timeBetweenFrames;
			}
		}
		if (player) {


			//cam.GetComponent<PostProcessingBehaviour> ().profile = associatedPostFX;
		}
	}

	public TinkerAnimation GetCurrentAnimation(){
		return animations [currentAnimation];
	}

	public string GetAnimationName(){
		return animations [currentAnimation].name;
	}
}
