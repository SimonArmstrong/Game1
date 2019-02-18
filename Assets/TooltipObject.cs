using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipObject : MonoBehaviour {
    public Item item;
    public TextMeshProUGUI titleText;
    public Image itemImage;
	
	// Update is called once per frame
	void Update () {
        titleText.text = item.ItemName;
        //itemImage.sprite = item.iconAnim.sprites[0];
	}
}
