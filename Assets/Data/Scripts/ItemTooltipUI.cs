using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltipUI : MonoBehaviour {
    public Item item;
    public TMPro.TextMeshProUGUI nameText;
    public Image itemImage;

    public void Update()
    {
        itemImage.sprite = item.iconAnim.sprites[0];
        nameText.text = item.name;
    }
}
