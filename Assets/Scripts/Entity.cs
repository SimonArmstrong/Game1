using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    [SerializeField]
    public Model model;

    [Header("Stats")]
    public float mp;
    public float hp;

    public Attributes attributes;
    public Stats stats;
    public GameObject shadow;

    //[Header("Conditions")]

    public Vector2 dirVec;
    public Direction direction = Direction.forward;

    public delegate Vector2 Mover();
    public Mover mover;

    public float rootMotionSpeed = 1;
    
    Vector2[] dirs = new Vector2[] {
        Vector2.down,
        Vector2.right,
        Vector2.up,
        Vector2.left,
        Vector2.zero
    };

    float dirChangeInterval = 3;
    float dirChangeTimer = 3;
    float walkTime = 0.2f;
    float walkTimer = 0.2f;
    int i = 4;

    public Vector2 StandStill() {
        return Vector2.zero;
    }

    public Vector2 InputMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(x, y, 0).normalized;
        return movement;
    }

    public Vector2 WanderMovement()
    {
        dirChangeTimer -= Time.deltaTime;
        if (dirChangeTimer <= 0)
        {
            i = Random.Range(0, 5);
            dirChangeTimer = dirChangeInterval;
        }

        Vector2 dir = dirs[i];
        Vector3 movement = new Vector3(dir.x, dir.y, 0).normalized;

        if (movement.magnitude > 0.1f)
        {
            walkTimer -= Time.deltaTime;
        }

        if (walkTimer <= 0)
        {
            movement = Vector3.zero;
            i = 4;
            walkTimer = walkTime;
        }

        return movement;
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

    public Vector2 GetFacingVec()
    {
        Vector3 dir = Vector2.zero;
        switch (direction) {
            case Direction.forward:
                dir = Vector2.down;
                break;
            case Direction.back:
                dir = Vector2.up;
                break;
            case Direction.left:
                dir = Vector2.left;
                break;
            case Direction.right:
                dir = Vector2.right;
                break;
        }

        return dir;
    }

    // Use this for initialization
    public virtual void Start () {
        mp = attributes.maxMP;
        hp = attributes.maxHP;
        mover = StandStill;

        if (transform.Find("shadow") != null)
            shadow = transform.Find("shadow").gameObject;
    }
    

    public virtual void Health(float amt) {
        hp += amt;
    }

    // Update is called once per frame
    public virtual void Update () {
        if (hp <= 0) {
            hp = 0;
            Die();
        }

        if (mp <= 0) {
            mp = 0;
        }


        if (hp >= attributes.maxHP)
            hp = attributes.maxHP;

        if (mp >= attributes.maxMP)
            mp = attributes.maxMP;
    }

    public virtual void FixedUpdate() {
        if (model != null) {
            direction = model.direction;
        }

        transform.position += (Vector3)dirVec.normalized * Time.deltaTime * attributes.moveSpeed * rootMotionSpeed;
    }

    public virtual void Die() {
        model.baseModel.destroyAfterPlayed = true;
    }

}
