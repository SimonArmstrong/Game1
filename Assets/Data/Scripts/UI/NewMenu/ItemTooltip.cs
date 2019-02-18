using UnityEngine;
using TMPro;
using System.Text;

public class ItemTooltip : MonoBehaviour {
    [SerializeField] TMP_Text ItemNameText;
    [SerializeField] TMP_Text ItemTypeText;
    [SerializeField] TMP_Text ItemDescriptionText;

    public void ShowTooltip(Item item){
        ItemNameText.text = item.ItemName;
        ItemTypeText.text = item.GetItemType();
        ItemDescriptionText.text = item.GetDescription();

        gameObject.SetActive(true);
    }


    public void HideTooltip(){
        gameObject.SetActive(false);
    }
    
}
