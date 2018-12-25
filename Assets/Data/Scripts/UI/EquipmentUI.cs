using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour {
    public UISlot head;
    public UISlot torso;
    public UISlot legs;
    public UISlot feet;


    float offsetX, offsetY;
    #region Singleton
    public static EquipmentUI instance;
    void Awake()
    {
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


    public void Refresh() {
        head.UpdateSlot();
        torso.UpdateSlot();
        legs.UpdateSlot();
        feet.UpdateSlot();
    }

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
