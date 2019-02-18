using System.Collections;
using UnityEngine;
using FromShadow.CharacterStats;

[CreateAssetMenu(menuName = "Item Effects/Stat Buff")]
public class StatBuffItemEffect : UsableItemEffect {

    public int StrengthBuff;
    public int AgilityBuff;
    public int IntelligenceBuff;
    public int VitalityBuff;
    public int LuckBuff;
    public int AttackBuff;
    public int DefenseBuff;
    public int CritBuff;
    public int MagicAttackBuff;
    public int MagicDefenseBuff;
    public int AttackSpeedBuff;

    public float Duration;

    public override void ExecuteEffect(UsableItem parentItem, Player player) {
        if (StrengthBuff != 0) {
            StatModifier strengthStatModifier = new StatModifier(StrengthBuff, StatModType.Flat, parentItem);
            player.Strength.AddModifier(strengthStatModifier);
            player.StartCoroutine(RemoveBuff(player, strengthStatModifier, Duration));
            player.character.UpdateStatValues();
        }
        if (AgilityBuff != 0) {
            StatModifier agilityStatModifier = new StatModifier(AgilityBuff, StatModType.Flat, parentItem);
            player.Agility.AddModifier(agilityStatModifier);
            player.StartCoroutine(RemoveBuff(player, agilityStatModifier, Duration));
            player.character.UpdateStatValues();
        }
        if (IntelligenceBuff != 0) {
            StatModifier intelligenceStatModifier = new StatModifier(IntelligenceBuff, StatModType.Flat, parentItem);
            player.Intelligence.AddModifier(intelligenceStatModifier);
            player.StartCoroutine(RemoveBuff(player, intelligenceStatModifier, Duration));
            player.character.UpdateStatValues();
        }
        if (VitalityBuff != 0) {
            StatModifier vitalityStatModifier = new StatModifier(VitalityBuff, StatModType.Flat, parentItem);
            player.Vitality.AddModifier(vitalityStatModifier);
            player.StartCoroutine(RemoveBuff(player, vitalityStatModifier, Duration));
            player.character.UpdateStatValues();
        }
        if (LuckBuff != 0) {
            StatModifier luckStatModifier = new StatModifier(LuckBuff, StatModType.Flat, parentItem);
            player.Dexterity.AddModifier(luckStatModifier);
            player.StartCoroutine(RemoveBuff(player, luckStatModifier, Duration));
            player.character.UpdateStatValues();
        }
        if (AttackBuff != 0) {
            StatModifier attackStatModifier = new StatModifier(AttackBuff, StatModType.Flat, parentItem);
            player.Attack.AddModifier(attackStatModifier);
            player.StartCoroutine(RemoveBuff(player, attackStatModifier, Duration));
            player.character.UpdateStatValues();
        }
        if (DefenseBuff != 0) {
            StatModifier defenseStatModifier = new StatModifier(DefenseBuff, StatModType.Flat, parentItem);
            player.Defense.AddModifier(defenseStatModifier);
            player.StartCoroutine(RemoveBuff(player, defenseStatModifier, Duration));
            player.character.UpdateStatValues();
        }
        if (CritBuff != 0) {
            StatModifier critStatModifier = new StatModifier(CritBuff, StatModType.Flat, parentItem);
            player.Crit.AddModifier(critStatModifier);
            player.StartCoroutine(RemoveBuff(player, critStatModifier, Duration));
            player.character.UpdateStatValues();
        }
        if (MagicAttackBuff != 0) {
            StatModifier magicAttackStatModifier = new StatModifier(MagicAttackBuff, StatModType.Flat, parentItem);
            player.MagicAttack.AddModifier(magicAttackStatModifier);
            player.StartCoroutine(RemoveBuff(player, magicAttackStatModifier, Duration));
            player.character.UpdateStatValues();
        }
        if (MagicDefenseBuff != 0) {
            StatModifier magicDefenseStatModifier = new StatModifier(MagicDefenseBuff, StatModType.Flat, parentItem);
            player.Toughness.AddModifier(magicDefenseStatModifier);
            player.StartCoroutine(RemoveBuff(player, magicDefenseStatModifier, Duration));
            player.character.UpdateStatValues();
        }
        if (AttackSpeedBuff != 0) {
            StatModifier attackSpeedStatModifier = new StatModifier(AttackSpeedBuff, StatModType.Flat, parentItem);
            player.AttackSpeed.AddModifier(attackSpeedStatModifier);
            player.StartCoroutine(RemoveBuff(player, attackSpeedStatModifier, Duration));
            player.character.UpdateStatValues();
        }
    }

    public override string GetDescription() {

        if (StrengthBuff != 0)
            return "Grants " + StrengthBuff + " Strength for " + Duration + " seconds.";
        if (AgilityBuff != 0)
            return "Grants " + AgilityBuff + " Agility for " + Duration + " seconds.";
        if (IntelligenceBuff != 0)
            return "Grants " + IntelligenceBuff + " Intelligence for " + Duration + " seconds.";
        if (VitalityBuff != 0)
            return "Grants " + VitalityBuff + " Vitality for " + Duration + " seconds.";
        if (LuckBuff != 0)
            return "Grants " + LuckBuff + " Luck for " + Duration + " seconds.";
        if (AttackBuff != 0)
            return "Grants " + AttackBuff + " Attack for " + Duration + " seconds.";
        if (DefenseBuff != 0)
            return "Grants " + DefenseBuff + " Defense for " + Duration + " seconds.";
        if (CritBuff != 0)
            return "Grants " + CritBuff + " Crit for " + Duration + " seconds.";
        if (MagicAttackBuff != 0)
            return "Grants " + MagicAttackBuff + " Magic Attack for " + Duration + " seconds.";
        if (MagicDefenseBuff != 0)
            return "Grants " + MagicDefenseBuff + " Magic Defense for " + Duration + " seconds.";
        if (AttackSpeedBuff != 0)
            return "Grants " + AttackSpeedBuff + " Attack Speed for " + Duration + " seconds.";

        return " ";
    }

    private static IEnumerator RemoveBuff(Player player, StatModifier statModifier, float duration){
        yield return new WaitForSeconds(duration);
        player.Strength.RemoveModifier(statModifier);
        player.Agility.RemoveModifier(statModifier);
        player.Intelligence.RemoveModifier(statModifier);
        player.Vitality.RemoveModifier(statModifier);
        player.Dexterity.RemoveModifier(statModifier);
        player.Attack.RemoveModifier(statModifier);
        player.Defense.RemoveModifier(statModifier);
        player.Crit.RemoveModifier(statModifier);
        player.MagicAttack.RemoveModifier(statModifier);
        player.Toughness.RemoveModifier(statModifier);
        player.AttackSpeed.RemoveModifier(statModifier);
        
        player.character.UpdateStatValues();
    }
}