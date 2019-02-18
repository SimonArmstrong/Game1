﻿using System.Collections;
using UnityEngine;
using FromShadow.CharacterStats;

[CreateAssetMenu(menuName = "Item Effects/Toughness Buff")]
public class MagicDefenseBuff : UsableItemEffect {

    public int buff;

    public float Duration;

    public override void ExecuteEffect(UsableItem parentItem, Player player) {
        if (buff != 0) {
            StatModifier statModifier = new StatModifier(buff, StatModType.Flat, parentItem);
            player.Toughness.AddModifier(statModifier);
            player.StartCoroutine(RemoveBuff(player, statModifier, Duration));
            player.character.UpdateStatValues();
        }
    }

    public override string GetDescription() {
        if (buff != 0)
            return "Grants " + buff + " Magic Defense for " + Duration + " seconds.";
        return " ";
    }

    private static IEnumerator RemoveBuff(Player player, StatModifier statModifier, float duration) {
        yield return new WaitForSeconds(duration);
        player.Toughness.RemoveModifier(statModifier);
        player.character.UpdateStatValues();
    }
}