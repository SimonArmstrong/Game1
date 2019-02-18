using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FromShadow.CharacterStats;

public enum GearType {
    HEAD,
    TORSO,
    HANDS,
    LEGS,
    FEET,
    WEAPON,
    BOW,
    SHIELD,
    RING,
    BELT,
    FACE_ACC,
    CONSUMABLE,
    QUEST,
    AMMO,
    TOOL
}
[CreateAssetMenu(menuName = "Items/Equippable Item")]
public class EquippableItem : Item {
    public TAnim[] anim;
    [Space]
    public int HealthBonus;
    public int ManaBonus;
    public int StaminaBonus;
    public int AttackBonus;
    public int MagicAttackBonus;
    public int BowAttackBonus;
    public int DefenseBonus;
    public int AttackSpeedBonus;
    public int VitalityBonus;
    public int CritBonus;
    public int SpiritBonus;
    public int EnduranceBonus;
    public int StrengthBonus;
    public int DexterityBonus;
    public int IntelligenceBonus;
    public int AgilityBonus;
    public int ToughnessBonus;
    [Space]
    public int HealthPercentBonus;
    public int ManaPercentBonus;
    public int StaminaPercentBonus;
    public int AttackPercentBonus;
    public int MagicAttackPercentBonus;
    public int BowAttackPercentBonus;
    public int DefensePercentBonus;
    public int AttackSpeedPercentBonus;
    public int VitalityPercentBonus;
    public int CritPercentBonus;
    public int SpiritPercentBonus;
    public int EndurancePercentBonus;
    public int StrengthPercentBonus;
    public int DexterityPercentBonus;
    public int IntelligencePercentBonus;
    public int AgilityPercentBonus;
    public int ToughnessPercentBonus;
    [Space]
    public GearType gearType;

    public Material material;

    public override Item GetCopy() {
        return Instantiate(this);
    }

    public override void Destroy() {
        Destroy(this);
    }

    public void Equip(Player c){
        // Flat + Add to stats
        if (HealthBonus != 0)
            c.Health.AddModifier(new StatModifier(HealthBonus, StatModType.Flat, this));
        if (ManaBonus != 0)
            c.Mana.AddModifier(new StatModifier(ManaBonus, StatModType.Flat, this));
        if (StaminaBonus != 0)
            c.Stamina.AddModifier(new StatModifier(StaminaBonus, StatModType.Flat, this));
        if (AttackBonus != 0)
            c.Attack.AddModifier(new StatModifier(AttackBonus, StatModType.Flat, this));
        if (MagicAttackBonus != 0)
            c.MagicAttack.AddModifier(new StatModifier(MagicAttackBonus, StatModType.Flat, this));
        if (BowAttackBonus != 0)
            c.BowAttack.AddModifier(new StatModifier(BowAttackBonus, StatModType.Flat, this));
        if (DefenseBonus != 0)
            c.Defense.AddModifier(new StatModifier(DefenseBonus, StatModType.Flat, this));
        if (AttackSpeedBonus != 0)
            c.AttackSpeed.AddModifier(new StatModifier(AttackSpeedBonus, StatModType.Flat, this));
        if (VitalityBonus != 0)
            c.Vitality.AddModifier(new StatModifier(VitalityBonus, StatModType.Flat, this));
        if (SpiritBonus != 0)
            c.Spirit.AddModifier(new StatModifier(SpiritBonus, StatModType.Flat, this));
        if (EnduranceBonus != 0)
            c.Endurance.AddModifier(new StatModifier(EnduranceBonus, StatModType.Flat, this));
        if (StrengthBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        if (DexterityBonus != 0)
            c.Dexterity.AddModifier(new StatModifier(DexterityBonus, StatModType.Flat, this));
        if (IntelligenceBonus != 0)
            c.Intelligence.AddModifier(new StatModifier(IntelligenceBonus, StatModType.Flat, this));
        if (AgilityBonus != 0)
            c.Agility.AddModifier(new StatModifier(AgilityBonus, StatModType.Flat, this));
        if (ToughnessBonus != 0)
            c.Toughness.AddModifier(new StatModifier(ToughnessBonus, StatModType.Flat, this));
        if (CritBonus != 0)
            c.Crit.AddModifier(new StatModifier(CritBonus, StatModType.Flat, this));
        

        // Percent multiplier for stats
        if (HealthPercentBonus != 0)
            c.Health.AddModifier(new StatModifier(HealthPercentBonus, StatModType.PercentMult, this));
        if (ManaPercentBonus != 0)
            c.Mana.AddModifier(new StatModifier(ManaPercentBonus, StatModType.PercentMult, this));
        if (StaminaPercentBonus != 0)
            c.Stamina.AddModifier(new StatModifier(StaminaPercentBonus, StatModType.PercentMult, this));
        if (AttackPercentBonus != 0)
            c.Attack.AddModifier(new StatModifier(AttackPercentBonus, StatModType.PercentMult, this));
        if (MagicAttackPercentBonus != 0)
            c.MagicAttack.AddModifier(new StatModifier(MagicAttackPercentBonus, StatModType.PercentMult, this));
        if (BowAttackPercentBonus != 0)
            c.BowAttack.AddModifier(new StatModifier(BowAttackPercentBonus, StatModType.PercentMult, this));
        if (DefensePercentBonus != 0)
            c.Defense.AddModifier(new StatModifier(DefensePercentBonus, StatModType.PercentMult, this));
        if (AttackSpeedPercentBonus != 0)
            c.AttackSpeed.AddModifier(new StatModifier(AttackSpeedPercentBonus, StatModType.PercentMult, this));
        if (VitalityPercentBonus != 0)
            c.Vitality.AddModifier(new StatModifier(VitalityPercentBonus, StatModType.PercentMult, this));
        if (SpiritPercentBonus != 0)
            c.Spirit.AddModifier(new StatModifier(SpiritPercentBonus, StatModType.PercentMult, this));
        if (EndurancePercentBonus != 0)
            c.Endurance.AddModifier(new StatModifier(EndurancePercentBonus, StatModType.PercentMult, this));
        if (StrengthPercentBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        if (DexterityPercentBonus != 0)
            c.Dexterity.AddModifier(new StatModifier(DexterityPercentBonus, StatModType.PercentMult, this));
        if (IntelligencePercentBonus != 0)
            c.Intelligence.AddModifier(new StatModifier(IntelligencePercentBonus, StatModType.PercentMult, this));
        if (AgilityPercentBonus != 0)
            c.Agility.AddModifier(new StatModifier(AgilityPercentBonus, StatModType.PercentMult, this));
        if (ToughnessPercentBonus != 0)
            c.Toughness.AddModifier(new StatModifier(ToughnessPercentBonus, StatModType.PercentMult, this));
        if (CritPercentBonus != 0)
            c.Crit.AddModifier(new StatModifier(CritPercentBonus, StatModType.PercentMult, this));

        c.OnEquip(this);
    }

    public void Unequip(Player c){
        c.OnUnequip(this);
        c.Health.RemoveAllModifiersFromSource(this);
        c.Mana.RemoveAllModifiersFromSource(this);
        c.Stamina.RemoveAllModifiersFromSource(this);
        c.Attack.RemoveAllModifiersFromSource(this);
        c.MagicAttack.RemoveAllModifiersFromSource(this);
        c.BowAttack.RemoveAllModifiersFromSource(this);
        c.Defense.RemoveAllModifiersFromSource(this);
        c.AttackSpeed.RemoveAllModifiersFromSource(this);
        c.Vitality.RemoveAllModifiersFromSource(this);
        c.Spirit.RemoveAllModifiersFromSource(this);
        c.Endurance.RemoveAllModifiersFromSource(this);
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Dexterity.RemoveAllModifiersFromSource(this);
        c.Intelligence.RemoveAllModifiersFromSource(this);
        c.Agility.RemoveAllModifiersFromSource(this);
        c.Toughness.RemoveAllModifiersFromSource(this);
        c.Crit.RemoveAllModifiersFromSource(this);
    }

    public override string GetItemType() {
        return gearType.ToString();
    }

    public override string GetDescription() {

        sb.Length = 0;
        AddStat(HealthBonus, "Health");
        AddStat(ManaBonus, "Mana");
        AddStat(StaminaBonus, "Stamina");
        AddStat(AttackBonus, "Attack");
        AddStat(MagicAttackBonus, "Magic Attack");
        AddStat(BowAttackBonus, "Bow Attack");
        AddStat(DefenseBonus, "Defense");
        AddStat(AttackSpeedBonus, "Attack Speed");
        AddStat(VitalityBonus, "Vitality");
        AddStat(SpiritBonus, "Spirit");
        AddStat(EnduranceBonus, "Endurance");
        AddStat(StrengthBonus, "Strength");
        AddStat(DexterityBonus, "Luck");
        AddStat(IntelligenceBonus, "Intelligence");
        AddStat(AgilityBonus, "Agility");
        AddStat(ToughnessBonus, "Toughness");
        AddStat(CritBonus, "Crit");

        AddStat(HealthPercentBonus, "Health", isPercent: true);
        AddStat(ManaPercentBonus, "Mana", isPercent: true);
        AddStat(StaminaPercentBonus, "Stamina", isPercent: true);
        AddStat(AttackPercentBonus, "Attack", isPercent: true);
        AddStat(MagicAttackPercentBonus, "Magic Attack", isPercent: true);
        AddStat(BowAttackPercentBonus, "Bow Attack", isPercent: true);
        AddStat(DefensePercentBonus, "Defense", isPercent: true);
        AddStat(AttackSpeedPercentBonus, "Attack Speed", isPercent: true);
        AddStat(VitalityPercentBonus, "Vitality", isPercent: true);
        AddStat(SpiritPercentBonus, "Spirit", isPercent: true);
        AddStat(EndurancePercentBonus, "Endurance", isPercent: true);
        AddStat(StrengthPercentBonus, "Strength", isPercent: true);
        AddStat(DexterityPercentBonus, "Luck", isPercent: true);
        AddStat(IntelligencePercentBonus, "Intelligence", isPercent: true);
        AddStat(AgilityPercentBonus, "Agility", isPercent: true);
        AddStat(ToughnessPercentBonus, "Toughness");
        AddStat(CritPercentBonus, "Crit", isPercent: true);
        return sb.ToString();
    }

    private void AddStat(float value, string statName, bool isPercent = false) {
        if (value != 0) {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            if (isPercent) {
                sb.Append(value);
                sb.Append("%");
            }
            else {
                sb.Append(value);
                sb.Append(" ");
            }

            sb.Append(" ");
            sb.Append(statName);
        }
    }
}
