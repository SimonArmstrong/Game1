using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Armour")]
public class Armour : Item {
    public float defense;
    public float weight;
    public GearType type;

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
