using UnityEngine;
using UnityEngine.Networking;
//using UnityEngine.PostProcessing;
using System.Collections;

public enum ANIMATIONS {
    IDLE,
    RUN,
    WALK,
    SWING1,
    SWING2,
    BLOCK, 
    INTERACT,
    CARRY_IDLE,
    CARRY_RUN,
    DIE
}

public class TinkerAnimator : MonoBehaviour {
    public TAnim[] animations;
    //public TinkerAnimation[] animations;
    public int currentAnimation = 0;
    public bool destroyAfterPlayed;
    public SpriteRenderer renderer;
    public bool player;
    public float animSpeedModifier = 1;
    public int dir;

    //public int frameIndex = 0;
    float timer;

    public int frameCount;

    public bool done;

    private bool isChild = false;
    private bool soundPlayed = false;
    private float soundCooldown = 0.3f;

    private bool dc = false;

    private int frameIdc = 0;
    private void Start() {

    }

    private void OnValidate()
    {

    }

    public void SwitchFrames(ref int frameIndex) {
        done = false;
        if (frameIdc != frameIndex)
            soundPlayed = false;
        //frameIndex++;
        // Reset frames if we've reached the end -- Creates Loop
        if (animations.Length < 1) return;
        // If we haven't involved a renderer, involove it
        if (renderer == null) {
            renderer = GetComponent<SpriteRenderer>();
        }

        if (animations[0] == null) {
            renderer.sprite = null;
            return;
        }

        if (animations.Length <= currentAnimation) return;

        if (currentAnimation >= animations.Length - 1) {
            //currentAnimation = animations.Length - 1;
        }

        frameCount = animations[currentAnimation].anims[dir].sprites.Length;

        if (frameIndex > frameCount - 1) {
            if (destroyAfterPlayed)
                Destroy(transform.parent.parent.gameObject);
            if (animations[currentAnimation].anims[dir].loop) {
                frameIndex = 0;
                done = false;
            } else {
                done = true;
                return;
                //frameIndex = animations[currentAnimation].anims[dir].sprites.Length;
            }
        }

        if (animations[currentAnimation].hasSound)
        {
            if (animations[currentAnimation].anims[dir].sound.Length > 0)
            {
                if (animations[currentAnimation].anims[dir].sound[frameIndex] != null)
                {
                    //animations[currentAnimation].anims[dir].sound[frameIndex] = 
                    if (!soundPlayed)
                        CreateSound(animations[currentAnimation].anims[dir].sound[frameIndex]);
                }
            }
        }

        if (animations[currentAnimation].anims[dir].damageCollider.Length > 0) {
            if (animations[currentAnimation].anims[dir].damageCollider[frameIndex])
                OpenDC();
            else
                CloseDC();
        }

        renderer.sprite = animations[currentAnimation].anims[dir].sprites[frameIndex];
        
        frameIdc = frameIndex;

    }

    public void OpenDC() {
        dc = true;
    }

    public void CloseDC() {
        dc = false;
    }

    public bool GetDC() {
        return dc;
    }

    private void Update() {
        if (animations.Length > 0) {
            //timer -= Time.deltaTime * animSpeedModifier;
            //if (timer <= 0) {
            //	frameIndex++;
            //	SwitchFrames ();
            //	timer = animations[currentAnimation].anims[dir].timeBetweenFrames;
            //}
        }
        if (player) {
            //if (associatedPostFX == null) {
            //associatedPostFX = Resources.Load ("Post Processing/default") as PostProcessingProfile;
            //}

            //cam.GetComponent<PostProcessingBehaviour> ().profile = associatedPostFX;
        }

        soundCooldown -= Time.deltaTime;
    }

    public TinkerAnimation GetCurrentAnimation() {
        return animations[currentAnimation].anims[dir];
    }

    public string GetAnimationName() {
        return animations[currentAnimation].anims[dir].name;
    }

    public float GetRootMotion() {
        float result = 0;
        if (frameIdc <= animations[currentAnimation].anims[dir].motionMultipliers.Length - 1)
            result = animations[currentAnimation].anims[dir].motionMultipliers[frameIdc];

        return result;
    }

    public void CreateSound(AudioClip clip) {
        AudioSource a = Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        a.clip = clip;
        a.volume = 0.17f;
        soundPlayed = true;
    }

    public bool Done() {
        //soundPlayed = false;
        return done;
    }
}
