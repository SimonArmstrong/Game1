using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using FromShadow.CharacterStats;

public class Entity : EntityNEW {
    [SerializeField]
    public Model model;

    [Header("Stats")]
    public float mp;
    public float hp;

    public Attributes attributes;
    //public Stats stats;

    [Header("Conditions")]
    public List<StatusCondition> conditions = new List<StatusCondition>();

    [Header("States")]
    public bool isStunned = false;

    public List<GameObject> conditionEffects = new List<GameObject>();
    public List<float> conditionDurations = new List<float>();
    public List<float> conditionTicks = new List<float>();

    public Vector2 dirVec;
    public Direction direction = Direction.forward;
    

    public delegate Vector2 Mover();
    public Mover mover;

    public float rootMotionSpeed = 1;
    public Rigidbody2D rb;

    public string objPoolTag;

    public Unit unit;

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
        //GamePadState padState = GamePad.GetState(PlayerIndex.One);
        //float x = padState.ThumbSticks.Left.X;
        //float y = padState.ThumbSticks.Left.Y;
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
        #region //Warnings//
        if(objPoolTag == null)Debug.LogWarning("This object needs a objPoolTag to be spawned.", transform);
        #endregion
        mp = attributes.maxMP;
        hp = attributes.maxHP;
        mover = StandStill;
        unit = GetComponent<Unit>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    public override void OnHit(float amt) {
        hp += amt;
        if (Mathf.Abs(amt) > 0) {
            GameManager.instance.SpawnDmgNum(transform, Mathf.Abs(Mathf.RoundToInt(amt)));
        }
        if (hp <= 0) {
            hp = 0;
            Die();
        }
    }

    // Update is called once per frame
    public override void Update () {
        if (mp <= 0) {
            mp = 0;
        }

        if (hp >= attributes.maxHP)
            hp = attributes.maxHP;

        if (mp >= attributes.maxMP)
            mp = attributes.maxMP;

        base.Update();
    }

    int effectArraySize;
    public virtual void ResolveConditions() {
        for (int i = 0; i < conditions.Count; i++) {
            StatusCondition condition = conditions[i];
            switch (condition.type) {
                case StatusCondition.Type.DoT:
                    DoTCondition dot = (DoTCondition)condition;
                    
                    //float duration = condition.duration;
                    //float tickTimer = dot.tickLength;

                    conditionTicks[i] -= Time.deltaTime;
                    conditionDurations[i] -= Time.deltaTime;

                    if (conditionTicks[i] <= 0) {
                        OnHit(-(dot.strength));
                        conditionTicks[i] = dot.tickLength;
                    }

                    if (conditionDurations[i] <= 0) {
                        conditions.RemoveAt(i);
                        Destroy(conditionEffects[i]);
                        conditionEffects.RemoveAt(i);
                        conditionDurations.RemoveAt(i);
                        conditionTicks.RemoveAt(i);
                    }
                    break;
                case StatusCondition.Type.Effect:
                    //float timer = condition.duration;
                    Attributes prev = attributes;
                    conditionDurations[i] -= Time.deltaTime;
                    attributes *= condition.attributeMod;
                    if (conditionDurations[i] <= 0) {
                        attributes = prev;
                        conditions.RemoveAt(i);
                        Destroy(conditionEffects[i]);
                        conditionDurations.RemoveAt(i);
                        conditionEffects.RemoveAt(i);
                    }

                    break;
                case StatusCondition.Type.Stun:
                    //float stunDuration = condition.duration;
                    conditionDurations[i] -= Time.deltaTime;
                    isStunned = true;
                    if (conditionDurations[i] <= 0) {
                        conditions.RemoveAt(i);
                        Destroy(conditionEffects[i]);
                        conditionDurations.RemoveAt(i);
                        conditionEffects.RemoveAt(i);
                        isStunned = false;
                    }
                    break;
            }
        }
    }

    public virtual void FixedUpdate() {
        if (model != null) {
            direction = model.direction;
        }
        ResolveConditions();

        if (isStunned) return;
        transform.position += (Vector3)dirVec.normalized * Time.deltaTime * attributes.moveSpeed * rootMotionSpeed;
    }

    public virtual void OnTriggerEnter2D(Collider2D col) {
        /*
        DamageCollider dc = col.GetComponent<DamageCollider>();
        
        if (dc != null) {
            if (!dc.gameObject.activeSelf) return;
            if (dc.owner != this) {
                Health(-dc.damage);
                if (dc.conditions.Count > 0) {
                    foreach (StatusCondition c in dc.conditions) {
                        conditions.Add(c);
                        conditionDurations.Add(c.duration);
                        conditionEffects.Add(Instantiate(c.effectObject, transform.position, Quaternion.identity, transform));
                        if (c.type == StatusCondition.Type.DoT)
                            conditionTicks.Add(((DoTCondition)c).tickLength);
                    }
                }
                if (rb != null)
                    rb.AddForce((transform.position - (dc.owner != null ? dc.owner.transform.position : dc.transform.position)).normalized * 500);
            }
        }
        */
    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        /*
        DamageCollider dc = col.transform.GetComponent<DamageCollider>();

        if (dc != null)
        {
            if (!dc.gameObject.activeSelf) return;
            if (dc.owner != this)
            {
                Health(-dc.damage);
                if (dc.conditions.Count > 0)
                {
                    foreach (StatusCondition c in dc.conditions)
                    {
                        conditions.Add(c);
                        conditionDurations.Add(c.duration);
                        conditionEffects.Add(Instantiate(c.effectObject, transform.position, Quaternion.identity, transform));
                        if (c.type == StatusCondition.Type.DoT)
                            conditionTicks.Add(((DoTCondition)c).tickLength);
                    }
                }
                if (rb != null)
                    rb.AddForce((transform.position - (dc.owner != null ? dc.owner.transform.position : dc.transform.position)).normalized * 500);
            }
        }
        */
    }

    public virtual void Die() {
        //model.baseModel.destroyAfterPlayed = true;
        GameManager.instance.KillEntity(this.gameObject);
    }
}
