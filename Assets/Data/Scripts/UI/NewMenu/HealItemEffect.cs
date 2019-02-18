using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Heal")]
public class HealItemEffect : UsableItemEffect {

    public int HealAmount;

    public override void ExecuteEffect(UsableItem parentItem, Player player) {
        player.Health.curValue += HealAmount;
    }

    public override string GetDescription() {
        return "Heals for " + HealAmount + " health.";
    }
}
