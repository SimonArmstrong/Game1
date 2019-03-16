using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using FromShadow.CharacterStats;

[System.Serializable]
public class Equipment {
    public Armour head;
    public Armour torso;
    public Armour legs;
    public Armour feet;
}

public class Player : Entity {
    [SerializeField]
    //public CharacterData character;
    public PlayerPickupNotification ppn;
    public float pickupRadius;

    public CharacterStat Health;
    public CharacterStat Mana;
    public CharacterStat Stamina;
    public CharacterStat Attack;
    public CharacterStat MagicAttack;
    public CharacterStat BowAttack;
    public CharacterStat Defense;
    public CharacterStat AttackSpeed;
    public CharacterStat Vitality;
    public CharacterStat Crit;
    public CharacterStat Spirit;
    public CharacterStat Endurance;
    public CharacterStat Strength;
    public CharacterStat Dexterity;
    public CharacterStat Intelligence;
    public CharacterStat Agility;
    public CharacterStat Toughness;

    public Transform itemShowTransform;
    public PlayerSettings settings;
    private Vector3 mov;

    // The direction from our player to our mouse pos, or facing direction
    public Vector2 aimDir;

    //public Equipment equipment;
    //public EquipmentUI eqpUI;
    //public List<Item> inventory = new List<Item>();
    //public InventoryUI invUI;

    public Character character;

    public GameObject interactionTrigger;
    public PlayerInteractionTrigger trigger;

    public GameObject projectile;

    public int money = 0;

    Vector2 swingDirection = Vector2.down;

    [Header("Attacking")]
    public float comboWindowLength = 0.9f;
    public Weapon equippedWeapon;
    public ToolItem harvestItem;
    float comboWindowTimer = 0;

    [Header("Dodge Roll")]
    public float dashDistance = 1;
    public float rollSpeed = 6;

    [Header("States")]
    public bool isAttacking = false;
    public bool isDodging = false;
    public bool isSwingingTool = false;

    public bool canComboSwing = false;
    public bool canCancelDodge = false;
    public bool canCancelAttack = false;
    public bool canComboAttack = false;

    [Header("Options")]
    [Tooltip("")] public bool attackTowardsMouse = false;
    [Tooltip("")] public bool dodgeTowardsMouse = false;
    [Tooltip("Press and Release to attack")] public bool allowChargeAttack = false;

    bool attackInput;
    bool blockInput;
    bool dodgeInput;
    bool bowInput;

    ///////////////////////////////////////////////////////////

    AnimatedDamageCollider dcDown;
    AnimatedDamageCollider dcUp;
    AnimatedDamageCollider dcLeft;
    AnimatedDamageCollider dcRight;

    float dodgeCooldown = 0.1f;
    bool dodged = false;
    bool storedDodgeInput = false;
    float rollTime = .1f;
    Vector3 rollPosition;
    int combonum = 2;
    bool storedAttackInput = false;
    float resetHoldTimer = 1;

    ///////////////////////////////////////////////////////////

    void CreateCharacter() {
        //character = new CharacterData("Simon", GENDER.MALE);
    }

    void LoadCharacter()
    {
        //character = CharacterData.Load(character.username + "_000000");
    }

    public void LoadModelGear(SerializedItemData idObj, TinkerAnimator modelPart) {
        ItemDatabase db = GameManager.instance.itemDatabase;

        if (idObj != null)
        {
            if (idObj.itemID != -1)
                modelPart.animations[(int)ANIMATIONS.IDLE] = ((Armour)db.items[idObj.itemID]).idle;
            else
                modelPart.animations[(int)ANIMATIONS.IDLE] = null;

            if (idObj.itemID != -1)
                modelPart.animations[(int)ANIMATIONS.RUN] = ((Armour)db.items[idObj.itemID]).run;
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
        //eqpUI.head.item = equipment.head;
        //eqpUI.torso.item = equipment.torso;
        //eqpUI.legs.item = equipment.legs;
        //eqpUI.feet.item = equipment.feet;
        //eqpUI.Refresh();
    }

    public void RefreshInventory() {
        //for (int i = 0; i < invUI.size; i++)
        //{
        //    UISlot slot = invUI.slots[i];
        //    inventory[i] = slot.item;
        //    slot.UpdateSlot();
        //}
    }

    public void GiveItem(Item item) {
        //int freeSlot = 0;
        //for (int i = 0; i < inventory.Count; i++) {
        //    if (inventory[i] == null) { freeSlot = i; break; }
        //}
        //inventory[freeSlot] = item;
        //invUI.slots[freeSlot].item = inventory[freeSlot];
        //RefreshInventory();
    }

    public void ReloadModel() {
        //string CHAR_ANIMS = character.gender == GENDER.FEMALE ? "Character/Animations/Female/" : "Character/Animations/Male/";
        //string GEAR_ANIMS = character.gender == GENDER.FEMALE ? "Gear/Animations/Female/" : "Gear/Animations/Male/";
        //string genderPrefix = character.gender == GENDER.FEMALE ? "F" : "M";


        //model.baseModel.animations[0] = Resources.Load<TAnim>(CHAR_ANIMS + "Base/idle/IDLE_" + genderPrefix); 
        //model.baseModel.animations[1] = Resources.Load<TAnim>(CHAR_ANIMS + "Base/run/RUN_" + genderPrefix);

        // LoadModelGear(equipment.torso, model.torsoModel);
        // LoadModelGear(equipment.legs, model.bottomsModel);
        // LoadModelGear(equipment.feet, model.feetModel);
        // LoadModelGear(character.equipment.hair, model.hairModel);
        // LoadModelGear(character.equipment.eyes, model.eyesModel);

        if (equippedWeapon != null)
        {
            if (equippedWeapon.material != null)
                model.weapon1Model.GetComponent<SpriteRenderer>().material = equippedWeapon.material;
        }
        if (harvestItem != null)
        {
            if (harvestItem.material != null)
                model.toolModel.GetComponent<SpriteRenderer>().material = harvestItem.material;
        }
    }
    
    private void OnEnable()
    {
        //invUI = InventoryUI.instance;
        //eqpUI = EquipmentUI.instance;
        //
        //HealthHUD.instance.entity = this;
        //
        //invUI.Init();
        //RefreshInventory();
        //RefreshEquipment();
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {

    }

    private void HandleDodge()
    {
        if (isSwingingTool) return;
        if (Input.GetButtonDown("Dodge") && isDodging)
        {
            storedDodgeInput = true;
            Debug.Log("Dash Stored");
        }

        if (isStunned || isAttacking)
        {
            isDodging = false;
            return;
        }
        if (dodgeCooldown > 0)
        {
            isDodging = false;
            return;
        }

        if (Input.GetButtonDown("Dodge") && !isDodging || (storedDodgeInput && !isDodging))
        {
            isDodging = true;
            storedDodgeInput = false;
            rollPosition = transform.position + (dodgeTowardsMouse ? (Vector3)aimDir.normalized * dashDistance : (Vector3)dirVec.normalized * dashDistance);
            Debug.DrawLine(transform.position, rollPosition);
        }

        ////////////////
        // Interrupts // 
        ////////////////

        if (!isDodging) return;

        transform.position += (rollPosition - transform.position) * Time.deltaTime * rollSpeed;

        model.frameIndex = 0;
        //GetComponent<Collider2D>().enabled = false;

        dirVec = Vector3.zero;

        rollTime -= Time.deltaTime;

        //if ((rollPosition - transform.position).magnitude <= 0.3f) //GetComponent<Collider2D>().enabled = true;
        if ((rollPosition - transform.position).magnitude <= 0.05f || rollTime <= 0)
        {
            rollTime = .1f;
            isDodging = false;
            dodgeCooldown = 0.1f;
        }
    }

    private void HandleAttacks() {
        #region Prerequisites
        // Only handle if we have a weapon
        if (equippedWeapon == null) return;

        if (isDodging) return;
        if (isSwingingTool) return;

        dcDown.currentCombo = combonum - 2;
        dcUp.currentCombo = combonum - 2;
        dcLeft.currentCombo = combonum - 2;
        dcRight.currentCombo = combonum - 2;

        // Make sure we're not stunned
        if (isStunned) return;

        if (bowInput) return;

        if (attackInput && isAttacking && canCancelAttack) {
            storedAttackInput = true;
        }

        // Check for attack input
        if ((attackInput && !isAttacking) || (storedAttackInput && !isAttacking))
        {
            isAttacking = true;
            model.frameIndex = 0;
            model.SwitchFrames();
            model.ResetFrameTimer();
            swingDirection = attackTowardsMouse ? aimDir : dirVec;
            storedAttackInput = false;
        }

        //if(canCancelAttack)

        /////////////////////////
        //NOTE: Interrputions??//
        /////////////////////////
        // Just to make sure our attacking wasn't interrupted. Interruptions should happen before here.
        if (!isAttacking) {
            return;
        }
        #endregion


        //TODO: Attack speed = weaponSpeed + ( DEX x 0.2 ) + ( STR x 0.2 ) ???
        model.animSpeedModifier = equippedWeapon.attackSpeed * (1 + (Strength.Value * 0.01f));

        if (model.baseModel.currentAnimation != combonum)
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
                combonum = 1;
            }
            combonum++;
        }

        //dirVec = swingDirection;
    }

    private void HandleMovement() {
        if (isAttacking) return;
        if (isStunned) return;
        if (isDodging) return;
        if (isSwingingTool) return;
        if (bowInput) return;

        rootMotionSpeed = 1;

        if (dirVec.magnitude > 0.001f)
        {
            if (model.baseModel.currentAnimation != (int)ANIMATIONS.RUN) model.ChangeAnimation((int)ANIMATIONS.RUN);
        }
        else
        {
            if (model.baseModel.currentAnimation != (int)ANIMATIONS.IDLE)
            {
                model.ChangeAnimation((int)ANIMATIONS.IDLE);
                model.ResetFrameTimer();
            }
        }

        model.animSpeedModifier = (attributes.moveSpeed / 2.3f) * 2;

        //equipment.head = (Armour)eqpUI.head.item;
        //equipment.torso = (Armour)eqpUI.torso.item;
        //equipment.legs = (Armour)eqpUI.legs.item;
        //equipment.feet = (Armour)eqpUI.feet.item;
        //LoadModelGear(equipment.torso, model.torsoModel);
        //LoadModelGear(equipment.legs, model.bottomsModel);
        //LoadModelGear(equipment.feet, model.feetModel);

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

    private void HandleShooting() {
        if (isAttacking) return;
        if (isStunned) return;
        if (isDodging) return;
        if (bowInput) {
            LookAt(aimDir);

            if (Input.GetMouseButtonDown(0)) {
                Projectile p = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
                p.direction = aimDir;
                p.GetComponent<DamageCollider>().owner = this;
            }
        }
    }

    private void HandleHarvesting() {
        if (isAttacking) return;
        if (isStunned) return;
        if (isDodging) return;
        if (harvestItem == null) return;

        if (Input.GetButtonDown("Submit") && !isSwingingTool) {
            isSwingingTool = true;
        }

        if (isSwingingTool) {
            if (model.baseModel.currentAnimation != 4) {
                model.frameIndex = 0;
                model.SwitchFrames();
                model.ResetFrameTimer();
                model.ChangeAnimation(4);
            }

            model.CalculateDirection(dirVec);
            model.UpdateFrames();

            dirVec = Vector3.zero;

            if (model.baseModel.Done()) {
                isSwingingTool = false;
            }
        }
    }

    private void HandleConsumption() {
        if (isAttacking) return;
        if (isDodging) return;
        if (isStunned) return;
        if (isSwingingTool) return;
    }

    public void OnUnequip(EquippableItem item)
    {
        switch (item.gearType)
        {
            case GearType.HEAD:
                model.hatModel.gameObject.SetActive(false);
                break;
            case GearType.TORSO:
                model.torsoModel.gameObject.SetActive(false);
                break;
            case GearType.LEGS:
                model.bottomsModel.gameObject.SetActive(false);
                break;
            case GearType.FEET:
                model.feetModel.gameObject.SetActive(false);
                break;
            case GearType.TOOL:
                harvestItem = null;
                break;
            case GearType.WEAPON:
                equippedWeapon = null;

                if (dcDown != null) Destroy(dcDown.gameObject);
                if (dcUp != null) Destroy(dcUp.gameObject);
                if (dcLeft != null) Destroy(dcLeft.gameObject);
                if (dcRight != null) Destroy(dcRight.gameObject);
                break;
        }
    }

    public void OnEquip(EquippableItem item) {
        switch (item.gearType) {
            case GearType.HEAD:
                model.hatModel.gameObject.SetActive(true);
                if (item.anim.Length > 0)
                    model.hatModel.animations = item.anim;

                if(item.material != null)
                    model.hatModel.GetComponent<SpriteRenderer>().material = item.material;
                break;
            case GearType.TORSO:
                model.torsoModel.gameObject.SetActive(true);
                if (item.anim.Length > 0)
                    model.torsoModel.animations = item.anim;
                if (item.material != null)
                    model.torsoModel.GetComponent<SpriteRenderer>().material = item.material;
                break;
            case GearType.LEGS:
                model.bottomsModel.gameObject.SetActive(true);
                if (item.anim.Length > 0)
                    model.bottomsModel.animations = item.anim;
                if (item.material != null)
                    model.bottomsModel.GetComponent<SpriteRenderer>().material = item.material;
                break;
            case GearType.FEET:
                model.feetModel.gameObject.SetActive(true);
                if (item.anim.Length > 0)
                    model.feetModel.animations = item.anim;
                if (item.material != null)
                    model.feetModel.GetComponent<SpriteRenderer>().material = item.material;
                break;
            case GearType.TOOL:
                harvestItem = (ToolItem)item;
                model.toolModel.gameObject.SetActive(true);
                if (item.anim.Length > 0)
                    model.toolModel.animations = item.anim;

                if (item.material != null)
                    model.toolModel.GetComponent<SpriteRenderer>().material = item.material;
                break;
            case GearType.WEAPON:

                equippedWeapon = (Weapon)item;
                if (item.anim.Length > 0)
                    model.weapon1Model.animations = equippedWeapon.anim;
                if (item.material != null)
                    model.weapon1Model.GetComponent<SpriteRenderer>().material = equippedWeapon.material;

                if (dcDown != null) Destroy(dcDown.gameObject);
                if (dcUp != null) Destroy(dcUp.gameObject);
                if (dcLeft != null) Destroy(dcLeft.gameObject);
                if (dcRight != null) Destroy(dcRight.gameObject);
                // Initialize damage colliders if we haven't yet
                dcDown = Instantiate(equippedWeapon.dcDown, transform.position, Quaternion.identity, transform).GetComponent<AnimatedDamageCollider>();
                dcUp = Instantiate(equippedWeapon.dcUp, transform.position, Quaternion.identity, transform).GetComponent<AnimatedDamageCollider>();
                dcLeft = Instantiate(equippedWeapon.dcLeft, transform.position, Quaternion.identity, transform).GetComponent<AnimatedDamageCollider>();
                dcRight = Instantiate(equippedWeapon.dcRight, transform.position, Quaternion.identity, transform).GetComponent<AnimatedDamageCollider>();

                dcDown.gameObject.SetActive(false);
                dcUp.gameObject.SetActive(false);
                dcLeft.gameObject.SetActive(false);
                dcRight.gameObject.SetActive(false);

                dcDown.owner = this;
                dcUp.owner = this;
                dcLeft.owner = this;
                dcRight.owner = this;

                dcDown.damage = Attack.Value;
                dcUp.damage = Attack.Value;
                dcLeft.damage = Attack.Value;
                dcRight.damage = Attack.Value;

                dcUp.projectile = equippedWeapon.projectile;
                dcDown.projectile = equippedWeapon.projectile;
                dcLeft.projectile = equippedWeapon.projectile;
                dcRight.projectile = equippedWeapon.projectile;

                dcUp.Init();
                dcDown.Init();
                dcLeft.Init();
                dcRight.Init();

                if (equippedWeapon.conditions.Count > 0)
                {
                    dcDown.conditions.AddRange(equippedWeapon.conditions);
                    dcUp.conditions.AddRange(equippedWeapon.conditions);
                    dcLeft.conditions.AddRange(equippedWeapon.conditions);
                    dcRight.conditions.AddRange(equippedWeapon.conditions);
                }
                break;
        }
        model.SwitchFrames();
    }

    public void Interrupt() {
        isDodging = false;
        isAttacking = false;
        isSwingingTool = false;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDodging) {
            Debug.Log("Interrupt");
            Interrupt();
        }
        base.OnCollisionEnter2D(collision);
    }
    public override void FixedUpdate () {

        RefreshInventory();

        if (dcUp != null) dcUp.swingDirection = aimDir;
        if (dcDown != null) dcDown.swingDirection = aimDir;
        if (dcLeft != null) dcLeft.swingDirection = aimDir;
        if (dcRight != null) dcRight.swingDirection = aimDir;

        if (combonum > 2)
        {
            comboWindowTimer -= Time.deltaTime;
            if (comboWindowTimer <= 0)
            {
                if (!isAttacking)
                {
                    combonum = 2;
                    comboWindowTimer = comboWindowLength;
                }
            }
        }
        else
        {
            comboWindowTimer = comboWindowLength;
        }

        HandleDodge();
        HandleHarvesting();
        HandleAttacks();
        HandleMovement();
        HandleShooting();
        HandleConsumption();

        model.sortingOrder = -(int)(transform.position.y * GameManager.instance.sortingFidelity);
        //HealthHUD.instance.UpdateHearts();

        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, pickupRadius, Vector2.zero);
        for (int i = 0; i < hit.Length; i++) {
            if(hit[i].collider.GetComponent<InventoryPickup>() != null){
                hit[i].collider.gameObject.transform.position -= (hit[i].collider.transform.position - transform.position - (Vector3.down * 0.2f)).normalized * Time.deltaTime * 3.2f;
            }
        }

        base.FixedUpdate();
	}
    public override void Start () {

        base.Start();
        SceneManager.sceneLoaded += OnSceneLoad;
        //mover = InputMovement;
        //CreateCharacter();
        comboWindowTimer = comboWindowLength;
        DontDestroyOnLoad(gameObject);
        //LoadCharacter();
        ReloadModel();

        //character.player = this;
        trigger = interactionTrigger.GetComponent<PlayerInteractionTrigger>();
        
        //if (!hasAuthority) return;
        //GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }
    public override void Update()
    {
        attackInput = Input.GetButtonDown("Attack");
        bowInput = Input.GetMouseButton(1);
        dirVec = InputMovement();
        
        if(dodgeCooldown > 0) dodgeCooldown -= Time.deltaTime;

        if (Input.GetKey(KeyCode.R) && resetHoldTimer > 0)
        {
            resetHoldTimer -= Time.deltaTime;
            if (resetHoldTimer <= 0) {
                SceneManager.LoadScene(0);
                transform.position = new Vector3(0, -5, 0);
                resetHoldTimer = 1;
            }
        }
        else {
            resetHoldTimer = 1;
        }

        if (!attackTowardsMouse) aimDir = dirVec;
        else {
            Vector2 v = (Camera.main.ScreenPointToRay(Input.mousePosition).origin - transform.position);
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

        //if (Input.GetButtonDown("Equipment")) {
        //    eqpUI.gameObject.SetActive(!eqpUI.gameObject.activeSelf);
        //}
        //
        //if (Input.GetButtonDown("Inventory")) {
        //    invUI.gameObject.SetActive(!invUI.gameObject.activeSelf);
        //}
    }
}
