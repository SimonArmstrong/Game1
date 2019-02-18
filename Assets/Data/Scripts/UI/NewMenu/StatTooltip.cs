using UnityEngine;
using TMPro;
using System.Text;
using FromShadow.CharacterStats;

public class StatTooltip : MonoBehaviour {

    [SerializeField] TMP_Text StatNameText;
    [SerializeField] TMP_Text StatModifiersLabelText;
    [SerializeField] TMP_Text StatModifiersText;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip(CharacterStat stat, string statName){

        StatNameText.text = GetStatTopText(stat, statName);
        StatModifiersText.text = GetStatModifiersText(stat);

        gameObject.SetActive(true);
    }

    public void HideTooltip(){
        gameObject.SetActive(false);
    }

    private string GetStatTopText(CharacterStat stat, string statName){
        sb.Length = 0;
        sb.Append(statName);
        sb.Append(" ");
        sb.Append(stat.Value);
        for (int i = 0; i < stat.StatModifiers.Count; i++) {
            if (stat.Value != stat.BaseValue) {
                sb.Append(" (");
                sb.Append(stat.BaseValue);

                if (stat.Value > stat.BaseValue)
                    sb.Append("+");

                sb.Append(System.Math.Round(stat.Value - stat.BaseValue, 4));
                sb.Append(")");
                sb.AppendLine();
            }
        }
        return sb.ToString();
    }

    private string GetStatModifiersText(CharacterStat stat){
        sb.Length = 0;

        foreach(StatModifier mod in stat.StatModifiers){
            if (sb.Length > 0)
                sb.AppendLine();

            if(mod.Value > 0){
                sb.Append("+");
            }

            if (mod.Type == StatModType.Flat) {
                sb.Append(mod.Value);
            } else{
                sb.Append(mod.Value);
                sb.Append("%");
            }

            Item item = mod.Source as Item;

            if(item != null){
                sb.Append(" ");
                sb.Append(item.ItemName);
            }else{
                Debug.Log("Modifier is not an Item");
            }
        }

        return sb.ToString();
    }
}

