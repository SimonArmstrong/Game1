using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    public Image mainSection;
    public Image barSection;
    public int size = 30;
    public GameObject slotPrefab;
    public List<UISlot> slots = new List<UISlot>();

    #region Singleton
    public static InventoryUI instance;
    void Awake()
    {
        Debug.Log("INV");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void Init() {
        for (int i = 0; i < size; i++) {
            UISlot slot = Instantiate(slotPrefab, mainSection.transform).GetComponent<UISlot>();
            slots.Add(slot);
        }
    }

    public void Refresh()
    {
        
    }

    float offsetX, offsetY;

    public void BeginDrag()
    {
        offsetX = GetComponent<RectTransform>().anchoredPosition.x - Input.mousePosition.x;
        offsetY = GetComponent<RectTransform>().anchoredPosition.y - Input.mousePosition.y;
    }

    public void OnDrag()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
    }
}
