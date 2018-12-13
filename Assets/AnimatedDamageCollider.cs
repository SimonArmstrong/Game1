using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedDamageCollider : MonoBehaviour {
    public List<GameObject> colliders = new List<GameObject>();
    public Entity owner;
    public int currentFrame;

    private void Update()
    {
        currentFrame = owner.model.frameIndex;

        for (int i = 0; i < colliders.Count; i++) {
            if(colliders[i] != null)
                colliders[i].SetActive(false);
        }
        if (colliders[currentFrame] != null)
            colliders[currentFrame].SetActive(true);
    }
}
