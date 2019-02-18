using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item/Armour")]
public class Armour : EquippableItem {
    public float defense;
    public float weight;

    [Header("Animation Data")]
    public TAnim idle;
    public TAnim run;
    public TAnim walk;
    public TAnim swing1;
    public TAnim swing2;
    public TAnim block;
    public TAnim interact;
    public TAnim carryIdle;
    public TAnim carryRun;
}
