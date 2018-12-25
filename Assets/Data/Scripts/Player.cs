using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Equipment {
    public Armour head;
    public Armour torso;
    public Armour legs;
    public Armour feet;
}

public class Player : Entity {
    [SerializeField]
    public CharacterData character;
    public PlayerPickupNotification ppn;

    public Transform itemShowTransform;
    public Weapon equippedWeapon;


    private Vector3 mov;

    // The direction from our player to our mouse pos, or facing direction
    public Vector2 aimDir;

    public Equipment equipment;
    public EquipmentUI eqpUI;
    public List<Item> inventory = new List<Item>();
    public InventoryUI invUI;

    public GameObject interactionTrigger;
    private PlayerInteractionTrigger trigger;

    public int money = 0;

    Vector2 swingDirection = Vector2.down;

    [Header("States")]
    public bool isAttacking = false;
    public bool isDodging = false;

    public bool canComboSwing = false;
    public bool canCancelDodge = false;
    public bool canCancelAttack = false;
    public bool canComboAttack = false;

    [Header("Options")]
    [Tooltip("")] public bool attackTowardsMouse = false;
    [Tooltip("")] public bool dodgeTowardsMouse = false;
    [Tooltip("Press and Release to attack")] public bool allowChargeAttack = false;

    void CreateCharacter() {
        character = new CharacterData("Simon", GENDER.MALE);
    }

    void LoadCharacter()
    {
        character = CharacterData.Load(character.username + "_000000");
    }

    public void LoadModelGear(SerializedItemData idObj, TinkerAnimator modelPart) {
        ItemDatabase db = GameManager.instance.itemDatabase;

        if (idObj != null)
        {
            if (idObj.itemID != -1)
                modelPart.animations[(int)ANIMATIONS.IDLE] = ((Armour)db.data[idObj.itemID]).idle;
            else
                modelPart.animations[(int)ANIMATIONS.IDLE] = null;

            if (idObj.itemID != -1)
                modelPart.animations[(int)ANIMATIONS.RUN] = ((Armour)db.data[idObj.itemID]).run;
            else
                modelPart.animations[(int)ANIMATIONS.RUN] = null;
        }
    }

    public void LoadModelGear(Armour idObj, TinkerAnimator modelPart)
    {
        ItemDatabase db = GameManager.instance.itemDatabase;

        if (idObj != null)
        {
            modelPart.animations[(int)ANIMATIONS.IDLE] = (idObj).idle;
            modelPart.animations[(int)ANIMATIONS.RUN] = (idObj).run;
        }
        else
        {
            modelPart.animations[(int)ANIMATIONS.IDLE] = null;
            modelPart.animations[(int)ANIMATIONS.RUN] = null;
        }
    }

    public void RefreshEquipment() {
        eqpUI.head.item = equipment.head;
        eqpUI.torso.item = equipment.torso;
        eqpUI.legs.item = equipment.legs;
        eqpUI.feet.item = equipment.feet;
        eqpUI.Refresh();
    }

    public void RefreshInventory() {
        for (int i = 0; i < invUI.size; i++)
        {
            UISlot slot = invUI.slots[i];
            inventory[i] = slot.item;
            slot.UpdateSlot();
        }
    }

    public void GiveItem(Item item) {
        int freeSlot = 0;
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i] == null) { freeSlot = i; break; }
        }
        inventory[freeSlot] = item;
        invUI.slots[freeSlot].item = inventory[freeSlot];
        RefreshInventory();
    }
    
    public void ReloadModel() {
        string CHAR_ANIMS = character.gender == GENDER.FEMALE ? "Character/Animations/Female/" : "Character/Animations/Male/";
        string GEAR_ANIMS = character.gender == GENDER.FEMALE ? "Gear/Animations/Female/" : "Gear/Animations/Male/";
        string genderPrefix = character.gender == GENDER.FEMALE ? "F" : "M";


        //model.baseModel.animations[0] = Resources.Load<TAnim>(CHAR_ANIMS + "Base/idle/IDLE_" + genderPrefix); 
        //model.baseModel.animations[1] = Resources.Load<TAnim>(CHAR_ANIMS + "Base/run/RUN_" + genderPrefix);

        LoadModelGear(equipment.torso, model.torsoModel);
        LoadModelGear(equipment.legs, model.bottomsModel);
        LoadModelGear(equipment.feet, model.feetModel);
        LoadModelGear(character.equipment.hair, model.hairModel);
        LoadModelGear(character.equipment.eyes, model.eyesModel);
    }

    public override void Health(float amt)
    {
        base.Health(amt);
    }

    // Use this for initialization
    public override void Start () {
        base.Start();
        //mover = InputMovement;
        //CreateCharacter();
        DontDestroyOnLoad(gameObject);
        //LoadCharacter();
        ReloadModel();

        //HealthHUD.instance.UpdateHearts();

        trigger = interactionTrigger.GetComponent<PlayerInteractionTrigger>();
        //if (!hasAuthority) return;
        //GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }

    private void OnEnable()
    {
        invUI = InventoryUI.instance;
        eqpUI = EquipmentUI.instance;

        //HealthHUD.instance.entity = this;

        invUI.Init();
        RefreshInventory();
        RefreshEquipment();
    }

    private void OnLevelWasLoaded(int level)
    {
        ScreenFade.instance.color = new Color(0, 0, 0, 0);
    }

    AnimatedDamageCollider dcDown;
    AnimatedDamageCollider dcUp;
    AnimatedDamageCollider dcLeft;
    AnimatedDamageCollider dcRight;

    float dt = 1.0f;
    bool dodged = false;
    private void HandleDodge() {

        if (isStunned || isAttacking) {
            return;
        }

        if (Input.GetButtonDown("Dodge") && !isDodging) {
            isDodging = true;

            GetComponent<Rigidbody2D>().AddForce(dodgeTowardsMouse ? aimDir * 800 : dirVec * 800);
        }
        ////////////////
        // Interrupts // 
        ////////////////

        if (!isDodging) return;

        model.frameIndex = 0;
        //GetComponent<Collider2D>().enabled = false;

        dirVec = Vector3.zero;

        dt -= Time.deltaTime;
        if (dt <= 0.2f) //GetComponent<Collider2D>().enabled = true;
        if (dt <= 0)
        {
            isDodging = false;
            dt = 1.0f;
        }
    }
    int combonum = 2;
    private void HandleAttacks() {
        #region Prerequisites
        // Only handle if we have a weapon
        if (equippedWeapon == null) return;

        if (isDodging) return;

        // Initialize damage colliders if we haven't yet
        if (dcDown == null) dcDown =   Instantiate(equippedWeapon.dcDown, transform.position, Quaternion.identity,  transform).GetComponent<AnimatedDamageCollider>();
        if (dcUp == null) dcUp =       Instantiate(equippedWeapon.dcUp, transform.position, Quaternion.identity,    transform).GetComponent<AnimatedDamageCollider>();
        if (dcLeft == null) dcLeft =   Instantiate(equippedWeapon.dcLeft, transform.position, Quaternion.identity,  transform).GetComponent<AnimatedDamageCollider>();
        if (dcRight == null) dcRight = Instantiate(equippedWeapon.dcRight, transform.position, Quaternion.identity, transform).GetComponent<AnimatedDamageCollider>();

        dcDown.gameObject.SetActive(false);
        dcUp.gameObject.SetActive(false);
        dcLeft.gameObject.SetActive(false);
        dcRight.gameObject.SetActive(false);

        dcDown.owner = this;
        dcUp.owner = this;
        dcLeft.owner = this;
        dcRight.owner = this;

        dcDown.damage = equippedWeapon.attack;
        dcUp.damage = equippedWeapon.attack;
        dcLeft.damage = equippedWeapon.attack;
        dcRight.damage = equippedWeapon.attack;

        if (equippedWeapon.conditions.Count > 0) {
            dcDown.conditions.AddRange(equippedWeapon.conditions);
            dcUp.conditions.AddRange(equippedWeapon.conditions);
            dcLeft.conditions.AddRange(equippedWeapon.conditions);
            dcRight.conditions.AddRange(equippedWeapon.conditions);
        }


        // Make sure we're not stunned
        if (isStunned) return;
        

        // Check for attack input
        if (Input.GetButtonDown("Attack") && !isAttacking)
        {
            isAttacking = true;
            combonum = 2;
            model.frameIndex = 0;
            swingDirection = attackTowardsMouse ? aimDir : dirVec;
        }


        /////////////////////////
        //NOTE: Interrputions??//
        /////////////////////////
        // Just to make sure our attacking wasn't interrupted. Interruptions should happen before here.
        if (!isAttacking) return;
        #endregion

        if ((!Input.GetButton("Attack")) || !allowChargeAttack)
        {
            if(canCancelAttack) 

            //TODO: Attack speed = weaponSpeed + ( DEX x 0.2 ) + ( STR x 0.2 ) ???
            model.animSpeedModifier = equippedWeapon.attackSpeed + (attributes.dexterity * 0.2f) + (attributes.strength * 0.2f);
            
            model.ChangeAnimation(combonum); // Attack anim
            model.CalculateDirection(swingDirection);
            model.UpdateFrames();

            dirVec = swingDirection;

            #region DamageCollider
            if (model.baseModel.GetDC())
            {
                switch (direction)
                {
                    case Direction.forward:
                        dcDown.gameObject.SetActive(true);
                        break;
                    case Direction.back:
                        dcUp.gameObject.SetActive(true);
                        break;
                    case Direction.left:
                        dcLeft.gameObject.SetActive(true);
                        break;
                    case Direction.right:
                        dcRight.gameObject.SetActive(true);
                        break;
                }
            }
            else
            {
                dcUp.End();
                dcDown.End();
                dcLeft.End();
                dcRight.End();
                dcUp.gameObject.SetActive(false);
                dcDown.gameObject.SetActive(false);
                dcLeft.gameObject.SetActive(false);
                dcRight.gameObject.SetActive(false);
            }
            #endregion

            rootMotionSpeed = model.baseModel.GetRootMotion();

            canCancelAttack = model.frameIndex >= 4;
            canComboAttack = model.frameIndex >= 6;

            if (model.baseModel.Done())
            {
                isAttacking = false;
                if (combonum > 2) {
                    combonum = 2;
                }
            }
        }
        //dirVec = swingDirection;
    }

    private void HandleMovement() {
        if (isAttacking) return;
        if (isStunned) return;
        if (isDodging) return;

        rootMotionSpeed = 1;

        if (dirVec.magnitude > 0.001f)
        {
            model.ChangeAnimation((int)ANIMATIONS.RUN);
        }
        else
        {
            model.ChangeAnimation((int)ANIMATIONS.IDLE);
        }

        model.animSpeedModifier = (attributes.moveSpeed / 2.3f) * 2;

        equipment.head = (Armour)eqpUI.head.item;
        equipment.torso = (Armour)eqpUI.torso.item;
        equipment.legs = (Armour)eqpUI.legs.item;
        equipment.feet = (Armour)eqpUI.feet.item;
        LoadModelGear(equipment.torso, model.torsoModel);
        LoadModelGear(equipment.legs, model.bottomsModel);
        LoadModelGear(equipment.feet, model.feetModel);

        Vector3 movement = dirVec;

        model.CalculateDirection(mov);
        model.UpdateFrames();

        //if (!isLocalPlayer) return;
        mov = movement;
        //if (!hasAuthority) return;


        switch (model.direction)
        {
            case Direction.forward:
                interactionTrigger.transform.localPosition = new Vector3(0.0f, -0.5f, 0.0f);
                break;
            case Direction.right:
                interactionTrigger.transform.localPosition = new Vector3(0.5f, 0.0f, 0.0f);
                break;
            case Direction.back:
                interactionTrigger.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
                break;
            case Direction.left:
                interactionTrigger.transform.localPosition = new Vector3(-0.5f, 0.0f, 0.0f);
                break;
        }

    }

    // Update is called once per frame
    public override void FixedUpdate () {

        RefreshInventory();

        //HandleDodge();
        HandleAttacks();
        HandleMovement();

        model.sortingOrder = -(int)(transform.position.y * GameManager.instance.sortingFidelity);
        //HealthHUD.instance.UpdateHearts();

        base.FixedUpdate();
	}

    public override void Update()
    {
        dirVec = InputMovement();
        if (!attackTowardsMouse) aimDir = dirVec;
        else {
            Vector2 v = (GameCursor.instance.worldPosition - transform.position);
            aimDir = v.normalized;
        }
        base.Update();
        if (Time.timeScale == 0) return;
        if (Input.GetButtonDown("Interact")) {
            if (trigger.interactable != null)
            {
                trigger.interactable.Interact(this);
            }
            if (trigger._NPC != null) {
                trigger._NPC.Interact(this);
            }
        }

        if (Input.GetButtonDown("Equipment")) {
            eqpUI.gameObject.SetActive(!eqpUI.gameObject.activeSelf);
        }

        if (Input.GetButtonDown("Inventory")) {
            invUI.gameObject.SetActive(!invUI.gameObject.activeSelf);
        }
    }
}
