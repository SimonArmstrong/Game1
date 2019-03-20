using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NPC : Interactable {
    [SerializeField]
    public Model model;

    private Vector3 mov;
    public Cutscene cutscene;

    public Dialogue dialogue;

    // Use this for initialization
    //public override void Start()
    //{
    //    base.Start();
    //    //mover = GetComponent<StateController>().Motion;
    //    //CreateCharacter();
    //    //LoadCharacter();
    //    //if (!hasAuthority) return;
    //    //GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    //}

    public override void Interact(Player p) {
        LookAt(p.transform.position);
        DialogueManager.instance.StartDialogue(dialogue);
    }

    public Vector2 LookAt(Vector3 target)
    {
        Vector3 dir = (transform.position - target).normalized;

        float dot1 = Vector3.Dot(dir, Vector3.up);      // 0 if either left or right, 1 if up, -1 if down
        float dot2 = Vector3.Dot(dir, Vector3.right);   // 1 if right, -1 if left, 0 if up or down

        Vector2 dot = new Vector2();

        if (Mathf.Abs(dot1) > Mathf.Abs(dot2)) dot = new Vector2(0, Mathf.RoundToInt(dot1));
        if (Mathf.Abs(dot1) <= Mathf.Abs(dot2)) dot = new Vector2(Mathf.RoundToInt(dot2), 0);

        if (dot == Vector2.left)
        {
            model.direction = Direction.right;
            model.UpdateDirection();
            model.SwitchFrames();
        }
        if (dot == Vector2.right)
        {
            model.direction = Direction.left;
            model.UpdateDirection();
            model.SwitchFrames();
        }
        if (dot == Vector2.down)
        {
            model.direction = Direction.back;
            model.UpdateDirection();
            model.SwitchFrames();
        }
        if (dot == Vector2.up)
        {
            model.direction = Direction.forward;
            model.UpdateDirection();
            model.SwitchFrames();
        }

        return dot;
    }
    // Update is called once per frame
    public void FixedUpdate()
    {
        if (mov.magnitude > 0.001f)
        {
            model.ChangeAnimation((int)ANIMATIONS.RUN);
        }
        else
        {
            model.ChangeAnimation((int)ANIMATIONS.IDLE);
        }
        //mov = ;
        //dirVec = GetComponent<StateController>().moveVec;
        model.CalculateDirection(mov);
        model.UpdateFrames();

        //rootMotionSpeed = model.baseModel.GetRootMotion();

        //if (!isLocalPlayer) return;
        //if (!hasAuthority) return;

        model.sortingOrder = -(int)(transform.position.y * GameManager.instance.sortingFidelity);
        //base.FixedUpdate();
    }

    public void Update()
    {
        mov = Vector3.zero;//GetComponent<StateController>().moveVec.normalized;

        if (Time.timeScale == 0) return;
    }
}
