using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Placeable Item")]
public class PlaceableItem : Item {

    public GameObject itemContainerObj;

    public void Place(Player p){
        Vector3 position = p.transform.position;
        position = new Vector3(Mathf.Round(position.x) / 2, Mathf.Round(position.y) / 2, 0);
        Instantiate(itemContainerObj, position, Quaternion.identity);
    }
}
