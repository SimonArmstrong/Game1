using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItemArea : MonoBehaviour, IDropHandler 
{
    public event System.Action OnDropEvent;

    public void OnDrop(PointerEventData eventData) {
        if (OnDropEvent != null){
            OnDropEvent();
        }
    }
}
