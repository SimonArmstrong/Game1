﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using FromShadow.CharacterStats;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private CharacterStat _stat;
    public CharacterStat Stat { 
        get { return _stat; }
        set{
            _stat = value;
            UpdateStatValue();
        }
    }
    

    private string _name;
    public string Name{ 
        get { return _name; }
        set {
            _name = value;
            nameText.text = _name;
        }
    }

    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text valueText;
    [SerializeField] StatTooltip tooltip;

    private void OnValidate() {
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        nameText = texts[1];
        valueText = texts[0];

        if (tooltip == null)
            tooltip = FindObjectOfType<StatTooltip>();
    }

    public void OnPointerEnter(PointerEventData eventData){
        tooltip.ShowTooltip(Stat, Name);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.HideTooltip();
    }

    public void UpdateStatValue() {
        valueText.text = _stat.Value.ToString();
    }
}
