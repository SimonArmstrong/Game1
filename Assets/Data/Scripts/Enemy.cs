using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {
    public Item[] drops;
    public SoundFont deathSound;
    public override void Start() {
        base.Start();

        mover = StandStill;
    }

    public override void FixedUpdate() { 
        if (dirVec.magnitude > 0.001f)
        {
            model.ChangeAnimation((int)ANIMATIONS.RUN);
        }
        else
        {
            model.ChangeAnimation((int)ANIMATIONS.IDLE);
        }
        //mov = ;
        //dirVec = GetComponent<StateController>().moveVec;
        model.CalculateDirection(dirVec);
        model.UpdateFrames();

        rootMotionSpeed = model.baseModel.GetRootMotion();

        //if (!isLocalPlayer) return;
        //if (!hasAuthority) return;

        model.sortingOrder = -(int)(transform.position.y * GameManager.instance.sortingFidelity);
        base.FixedUpdate();
    }

    public override void Die()
    {
        for (int i = 0; i < drops.Length; i++) {
            InventoryPickup item = Instantiate(GameManager.instance.genericItemDropObject, transform.position, Quaternion.identity).GetComponent<InventoryPickup>();
            item.item = drops[i];
        }
        if(deathSound != null)
            Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<SoundEffect>().soundFont = deathSound;

        GetComponent<Collider2D>().enabled = false;
        base.Die();
    }
}
