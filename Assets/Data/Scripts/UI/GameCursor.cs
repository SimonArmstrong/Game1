using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCursor : MonoBehaviour {
    public Object target;
    public TMPro.TextMeshProUGUI text;

    public Camera cam;

    public bool controller = false;
    public float cursorSpeed = 3;

    public Item targetItem;
    public GameObject tooltip;

    public Item held;
    public Image itemImage;

    public Vector3 worldPosition;

    public static GameCursor instance;

    // Use this for initialization
    void Start () {
        //Cursor.visible = false;
        instance = this;

        cam = Camera.main.transform.GetComponentInChildren<Camera>();
    }

    public void DropItem() {
        if (held == null) return;
        Instantiate(GameManager.instance.genericItemDropObject, worldPosition, Quaternion.identity).GetComponent<ItemDrop>().item = held;
        held = null;
    }

	// Update is called once per frame
	void Update () {
        if (controller)
        {
            Vector2 i = new Vector2(Input.GetAxis("RightAxis X"), Input.GetAxis("RightAxis Y"));
            GetComponent<RectTransform>().anchoredPosition += i * cursorSpeed;
        }
        else {
            GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;
        }

        Ray ray = cam.ScreenPointToRay(GetComponent<RectTransform>().anchoredPosition);
        worldPosition = ray.origin;
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            
        target = null;
        text.text = "";

        tooltip.SetActive(targetItem != null);
        if (targetItem != null) {
            tooltip.GetComponent<ItemTooltipUI>().item = targetItem;
        }

        if (held != null)
        {
            itemImage.color = new Color(1, 1, 1, 1);
            itemImage.sprite = held.icon;
        }

        if (held == null)
        {
            itemImage.color = new Color(0, 0, 0, 0);
            itemImage.sprite = null;
        }

        if (hit.collider == null) return;

        if (hit.collider.GetComponent<Entity>() != null)
        {
            target = hit.collider.gameObject;
            text.text = target.name;
        }
    }

    private void LateUpdate()
    {
        //if(Input.GetButtonUp())
    }
}
