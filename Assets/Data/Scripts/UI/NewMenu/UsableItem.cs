using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Usable Item")]
public class UsableItem : Item {

    public bool isConsumable;

    public List<UsableItemEffect> Effects;

	public virtual void Use(Player player){
        foreach(UsableItemEffect effect in Effects){
            effect.ExecuteEffect(this, player);
        }
    }

    public override string GetItemType() {
        return isConsumable ? "Consumable" : "Usable";
    }

    public override string GetDescription() {
        sb.Length = 0;

        foreach(UsableItemEffect effect in Effects){
            sb.AppendLine(effect.GetDescription());
        }
        return sb.ToString();
    }
}
