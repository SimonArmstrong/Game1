using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldDoor : MonoBehaviour {
    public int sceneID;

    Cutscene go;
    bool cutsceneStart = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity e = collision.collider.GetComponent<Entity>();
        if (e != null) {
            GameObject cutscene = GameManager.instance.genericCutsceneObject;
            go = Instantiate(cutscene).GetComponent<Cutscene>();
            go.cam.actor = Camera.main.gameObject;
            go.cam.positions.Add(transform);
            go.cam.transitionTimes.Add(5);
            go.cam.holdTimes.Add(1.3f);
            go.cam.screenColors.Add(new Color(0, 0, 0, 1));
            go.fadeIn = true;
            cutsceneStart = true;
        }
    }

    private void Update()
    {
        if(cutsceneStart)
        {

            if (go == null) {
                SceneManager.LoadScene(sceneID);
            }
        }
    }
}
