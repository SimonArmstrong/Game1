using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCutsceneTrigger : Interactable {
    public Transform t;
    public Transform t2;
    public AudioClip clip;
    public Item reward;

    public override void Interact(Player p)
    {
        base.Interact(p);
        //GetComponent<DialogueTrigger>().TriggerDialogue();
        Cutscene c = Instantiate(GameManager.instance.genericCutsceneObject, p.transform.position, Quaternion.identity).GetComponent<Cutscene>();
        Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().clip = clip;
        c.cam.actor = Camera.main.gameObject;
        c.reward = reward;
        c.cam.positions.Add(t);
        c.cam.transitionTimes.Add(4);
        c.cam.holdTimes.Add(4.3f);
    }
}
