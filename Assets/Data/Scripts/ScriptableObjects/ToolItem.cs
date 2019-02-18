using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Tool")]
public class ToolItem : EquippableItem {
    public enum Type {
        AXE, PICKAXE
    }
    public Type type;
    public int toolStrength = 1;

    public override void OnValidate() {
        gearType = GearType.TOOL;
        base.OnValidate();
    }
}
