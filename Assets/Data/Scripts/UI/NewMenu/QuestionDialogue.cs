using System;
using UnityEngine;

public class QuestionDialogue : MonoBehaviour {
    public event System.Action OnYesEvent;
    public event System.Action OnNoEvent;

    public void Show(){
        gameObject.SetActive(true);
        OnYesEvent = null;
        OnNoEvent = null;
    }

    public void Hide(){
        gameObject.SetActive(false);
    }

	public void OnYesButtonClicked(){
        if (OnYesEvent != null)
            OnYesEvent();

        Hide();
    }

    public void OnNoButtonClicked() {
        if (OnNoEvent != null)
            OnNoEvent();

        Hide();
    }
}
