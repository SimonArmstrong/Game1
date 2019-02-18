using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DropTable", menuName = "DropTable")]
public class DropTable : ScriptableObject {

    public DropPass[] dropPasses;

    ScriptableObject[] GetItems()
    {
        ScriptableObject[] returnList = new ScriptableObject[0];
        return returnList;
    }

    void OnValidate()
    {
        for (int i = 0; i < dropPasses.Length; i++)
        {
            float curScaledWeightDifference = 0f;
            float curTotalWeight = 0f;
            int curDropLength = 0;
            dropPasses[i].name = "Pass " + (i + 1);
            foreach(DropContainer drop in dropPasses[i].dropContainers)
            {
                #region UpdateItemNames
                if (drop.itemSO != null) drop.name = drop.itemSO.name;
                else drop.name = null;
                #endregion
                curTotalWeight += drop.dropWeight;
                if (drop.dropWeight > 0) curDropLength += 1;
            }
            curScaledWeightDifference = (100 - curTotalWeight) / curDropLength;
            foreach (DropContainer drop in dropPasses[i].dropContainers)
            {
                if (drop.dropWeight > 0) drop.dropPercent = drop.dropWeight + curScaledWeightDifference;
                //else drop.dropPercent = 0.0f;
            }
        }
    }
}

[System.Serializable]
public class DropContainer
{
    [HideInInspector]
    public string name;
    public ScriptableObject itemSO;
    public int quantity;
    [Range(0.01f, 100.0f)]
    public float dropWeight;
    public float dropPercent;
}

[System.Serializable]
public class DropPass
{
    [HideInInspector]
    public string name;
    public int passChance;
    public DropContainer[] dropContainers;
}
