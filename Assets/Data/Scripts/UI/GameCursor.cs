using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TInv;

public class GameCursor : MonoBehaviour {
    public Object target;
    public TMPro.TextMeshProUGUI text;

    public Camera cam;

    public bool controller = false;
    public float cursorSpeed = 3;

    public Item targetItem;
    public GameObject tooltip;

    public ItemData heldItem = null;
    public Image itemImage;

    public Vector3 worldPosition;

    public static GameCursor instance;

    public LayerMask raycastLayers;

    private void OnEnable()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        heldItem = null;
        cam = Camera.main.transform.GetComponentInChildren<Camera>();
    }

    public void DropItem() {
        if (heldItem == null) return;
        Instantiate(GameManager.instance.genericItemDropObject, worldPosition, Quaternion.identity).GetComponent<ItemDrop>().item = heldItem.item;
        heldItem = null;
    }

    public static ItemData HoldItem(ItemData item) {
        ItemData prevItem = instance.heldItem;
        instance.heldItem = item;

        instance.itemImage.sprite = null;
        instance.itemImage.color = new Color(0, 0, 0, 0);

        if (item == null) return prevItem;
        if (item.item == null) return prevItem;

        instance.itemImage.sprite = instance.heldItem.item.iconAnim.sprites[0];
        instance.itemImage.color = new Color(1, 1, 1, 1);

        return prevItem;
    }

    public static ItemData HoldItem(ItemData item, int amount)
    {
        ItemData prevItem = instance.heldItem;
        instance.heldItem = item;

        instance.itemImage.sprite = null;
        instance.itemImage.color = new Color(0, 0, 0, 0);

        if (item == null || item.item == null) {
            //prevItem = 
            return prevItem;
        }

        ItemData newItem = new ItemData(item.item, amount);
        ItemData itemRemainder = new ItemData(item.item, item.amount - amount);

        if (itemRemainder.amount <= 0) itemRemainder = null;

        instance.itemImage.sprite = instance.heldItem.item.iconAnim.sprites[0];
        instance.itemImage.color = new Color(1, 1, 1, 1);

        return itemRemainder;
    }

    float t = 0;
    // Update is called once per frame
    void Update () {
        t += Time.unscaledDeltaTime * 10;

        float x = 25 + Mathf.Cos(t) * 1.35f;
        float y = 25 + Mathf.Cos(t) * 1.35f;

        Vector2 v = new Vector2(Mathf.Abs(x), Mathf.Abs(y));
        GetComponent<RectTransform>().sizeDelta = v;

        //Debug.Log("x: " + x + ", y: " + y + ", t: " + t);
        if (controller)
        {
            //Vector2 i = new Vector2(Input.GetAxis("RightAxis X"), Input.GetAxis("RightAxis Y"));
            //GetComponent<RectTransform>().anchoredPosition += i * cursorSpeed;
        }
        else {
            GetComponent<RectTransform>().position = Input.mousePosition;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        worldPosition = ray.origin;
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 999f, raycastLayers);
        target = null;
        text.text = "";

        //tooltip.SetActive(targetItem != null);
        if (targetItem != null) {
            //tooltip.GetComponent<ItemTooltipUI>().item = targetItem;
        }

        if (hit.collider == null) return;

        Interactable inter = hit.collider.GetComponent<Interactable>();
        NPC npc = hit.collider.GetComponent<NPC>();

        if (inter != null)
        {
            Player p = GameManager.instance.player.GetComponent<Player>();
            target = hit.collider.gameObject;
            text.text = target.name;
            if ((inter.transform.position - p.transform.position).magnitude <= 1.0f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    inter.Interact(p);
                }
            }
        }

        if (npc != null) {
            Player p = GameManager.instance.player.GetComponent<Player>();
            target = hit.collider.gameObject;
            text.text = target.name;
            if ((npc.transform.position - p.transform.position).magnitude <= 1.0f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    npc.Interact(p);
                }
            }
        }
    }

    private void LateUpdate()
    {
        //if(Input.GetButtonUp())
    }
}
