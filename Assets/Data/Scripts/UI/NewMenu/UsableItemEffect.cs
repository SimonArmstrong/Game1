using UnityEngine;

public abstract class UsableItemEffect : ScriptableObject {
    public abstract void ExecuteEffect(UsableItem parentItem, Player character);
    public abstract string GetDescription();
}