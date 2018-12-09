using UnityEngine;
using UnityEngine.Networking;
//using UnityEngine.PostProcessing;
using System.Collections;

public class SimpleTinkerAnimator : MonoBehaviour
{
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


    private void Start()
    {

    }

    public void SwitchFrames(ref int frameIndex)
    {
        //frameIndex++;
        // Reset frames if we've reached the end -- Creates Loop
        if (animations.Length < 1) return;

        // If we haven't involved a renderer, involove it
        if (renderer == null)
        {
            renderer = GetComponent<SpriteRenderer>();
        }


        frameCount = animations[currentAnimation].anims[dir].sprites.Length;

        if (frameIndex > frameCount - 1)
        {
            if (destroyAfterPlayed)
                Destroy(gameObject);
            if (animations[currentAnimation].anims[dir].loop)
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

        if (animations[currentAnimation].hasSound)
        {
            if (animations[currentAnimation].anims[dir].sound.Length > 0)
            {
                if (animations[currentAnimation].anims[dir].sound[frameIndex] != null)
                {
                    if (soundCooldown <= 0)
                        CreateSound(animations[currentAnimation].anims[dir].sound[frameIndex]);
                }
            }
        }

        renderer.sprite = animations[currentAnimation].anims[dir].sprites[frameIndex];
    }

    int p_frameIndex;
    public void UpdateFrames()
    {
        if (animations.Length > 0)
        {
            timer -= Time.deltaTime * animSpeedModifier;
            if (timer <= 0) {
                p_frameIndex++;
            	SwitchFrames (ref p_frameIndex);
            	timer = animations[currentAnimation].anims[dir].timeBetweenFrames;
            }
        }

        soundCooldown -= Time.deltaTime;
    }

    public TinkerAnimation GetCurrentAnimation()
    {
        return animations[currentAnimation].anims[dir];
    }

    public string GetAnimationName()
    {
        return animations[currentAnimation].anims[dir].name;
    }

    public void CreateSound(AudioClip clip)
    {
        AudioSource a = Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        a.clip = clip;
        a.volume = 0.1f;
        soundCooldown = 0.2f;
    }
}
