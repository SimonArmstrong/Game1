using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPoint : MonoBehaviour {

    public GameObject goToSpawn;
    public string tag;


    void OnValidate()
    {
        #region Update tag
        Entity e;
        if (goToSpawn == null) return;
        if (goToSpawn.GetComponent<Entity>() != null)
        {
            e = goToSpawn.GetComponent<Entity>();
            if (e.objPoolTag != null) tag = e.objPoolTag;
            else Debug.LogWarning("This Entity needs a objPoolTag to be spawned.", goToSpawn.transform);
        }
        #endregion
    }
}
