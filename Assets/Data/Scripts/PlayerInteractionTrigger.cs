using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionTrigger : MonoBehaviour {
    public Animator popupAnimator;
    public Interactable interactable;
    public NPC _NPC;
    public AudioClip interactionSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable target = collision.gameObject.GetComponent<Interactable>();
        NPC npc = collision.gameObject.GetComponent<NPC>();

        if (target != null) {
            popupAnimator.SetBool("Interaction", true);
            interactable = target;
            Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().clip = interactionSound;
        }

        if (npc != null)
        {
            popupAnimator.SetBool("Interaction", true);
            _NPC = npc;
            Instantiate(GameManager.instance.genericSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().clip = interactionSound;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable target = collision.gameObject.GetComponent<Interactable>();
        NPC npc = collision.gameObject.GetComponent<NPC>();

        if (target != null) {
            popupAnimator.SetBool("Interaction", false);
            interactable = null;
        }
        if (npc != null) {
            popupAnimator.SetBool("Interaction", false);
            _NPC = null;
        }
    }
}
