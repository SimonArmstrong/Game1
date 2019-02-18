using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    public Image image;
    public TMP_Text amountText;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;

    public List<Action<BaseItemSlot>> PossibleEvents = new List<Action<BaseItemSlot>>();

    protected bool isPointerOver;

    protected Color normalColor = Color.white;
    protected Color disabledColor = new Color(1, 1, 1, 0);

    protected Item _item;
    public Item Item {
        get { return _item; }
        set {
            _item = value;
            if (_item == null && Amount != 0) Amount = 0;

            if (_item == null) {
                image.color = disabledColor;
            }
            else {
                image.sprite = _item.iconAnim.sprites[0];
                image.preserveAspect = true; 
                image.color = normalColor;
                if(image.GetComponent<TinkerAnimator2>() == null)
                    return;
                image.GetComponent<TinkerAnimator2>().animations = new TinkerAnimation[1];
                image.GetComponent<TinkerAnimator2>().animations[0] =_item.iconAnim;
            }
            if(isPointerOver){
                OnPointerExit(null);
                OnPointerEnter(null);
            }
        } 
    }

    private int _amount;
    public int Amount {
        get { return _amount; }
        set {
            _amount = value;
            if (_amount < 0) _amount = 0;
            if (_amount == 0 && Item != null) Item = null;

            if (amountText != null) {
                amountText.enabled = _item != null && _amount > 1;
                if (amountText.enabled) {
                    amountText.text = _amount.ToString();
                }
            }
        }
    }

    protected virtual void OnValidate() {
        if (image == null)
            image = GetComponent<Image>();

        if (amountText == null) {
            amountText = GetComponentInChildren<TMP_Text>();
        }
    }

    public virtual bool CanAddStack(Item item, int amount = 1){
        return Item != null && Item.ID == item.ID;
    }

    public virtual bool CanReceiveItem(Item item) {
        return false;
    }

    protected virtual void OnDisable() {
        if(isPointerOver)
            OnPointerExit(null);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right) {
            if (OnRightClickEvent != null) {
                OnRightClickEvent(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        isPointerOver = true;
        if (OnPointerEnterEvent != null) {
            OnPointerEnterEvent(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        isPointerOver = false;
        if (OnPointerExitEvent != null) {
            OnPointerExitEvent(this);
        }
    }

    private void Update() {
    }
}
