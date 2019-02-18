using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NPC : Entity
{
    private Vector3 mov;
    public Cutscene cutscene;

    public Dialogue dialogue;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        //mover = GetComponent<StateController>().Motion;
        //CreateCharacter();
        //LoadCharacter();
        //if (!hasAuthority) return;
        //GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }

    public virtual void Interact(Player p) {
        LookAt(p.transform.position);
        DialogueManager.instance.StartDialogue(dialogue);
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
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

    public override void Update()
    {
        dirVec = Vector3.zero;//GetComponent<StateController>().moveVec.normalized;

        base.Update();
        if (Time.timeScale == 0) return;
    }
}
