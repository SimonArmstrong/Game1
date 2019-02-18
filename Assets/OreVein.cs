using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreVein : EntityNEW {
    public float durability = 8;
    public Item ore;
    public int amount;



    public override void OnHit(float p_amt)
    {
        durability--;
        if (durability <= 0) return;
        Instantiate(GameManager.instance.genericItemDropObject, transform.position, Quaternion.identity).GetComponent<InventoryPickup>().item = ore;
        base.OnHit(p_amt);
    }
}
