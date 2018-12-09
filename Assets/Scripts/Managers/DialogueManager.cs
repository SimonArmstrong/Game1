using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI dialogueText;

    public float heightDown;
    public float heightUp;

    bool isOpen = false;

    bool endOfSentence = true;

    Queue<Dialogue> dialogues;

    Queue<Sentence> sentences;
    Sentence sentence = null;
    string sentenceFill = null;

    #region Singleton
    public static DialogueManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    void Start () {
        sentences = new Queue<Sentence>();
        dialogues = new Queue<Dialogue>();
	}

    private void Update()
    {
        float targetHeight = isOpen ? heightUp : heightDown;
        GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, new Vector2(GetComponent<RectTransform>().anchoredPosition.x, targetHeight), Time.unscaledDeltaTime * 500);

        if (Input.GetButtonUp("Interact")) {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isOpen = true;

        dialogues.Clear();

        GameManager.instance.ChangeState(GameStates.cutscene);
        nameText.text = dialogue.dialogueData.displayName;

        sentences.Clear();

        foreach(Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        endOfSentence = true;
        DisplayNextSentence();
    }

    public void StartDialogue(Dialogue[] dialogue)
    {
        isOpen = true;

        GameManager.instance.ChangeState(GameStates.cutscene);

        dialogues.Clear();

        foreach (Dialogue log in dialogue)
        {
            dialogues.Enqueue(log);
        }

        Dialogue curDialogue = dialogues.Dequeue();

        nameText.text = curDialogue.dialogueData.displayName;

        sentences.Clear();
        
        foreach (Sentence sentence in curDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        endOfSentence = true;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0 && endOfSentence == true)
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();
        if (endOfSentence == true)
        {
            sentence = sentences.Dequeue();
            if (sentence.prevSentenceClear)
            {
                sentenceFill = sentence.sentence;
            }
            else {
                sentenceFill += sentence.sentence;
            }
            Debug.Log("Begin text scroll, current sentence: " + sentence.sentence);
            StartCoroutine(TypeSentence(sentence));
        }
        else
        {
            Debug.Log("Skipped text scroll, current sentence: " + sentence.sentence);
            dialogueText.text = sentenceFill;
            endOfSentence = true;
        }

    }

    IEnumerator TypeSentence (Sentence sentence)
    {
        if (sentence.prevSentenceClear) dialogueText.text = "";
        char[] curSentence = sentence.sentence.ToCharArray();
        foreach(char letter in curSentence)
        {
            //if (dialogueText.text.Length == curSentence.Length) endOfSentence = true;
            endOfSentence = false;
            dialogueText.text += letter;
            yield return null;
        }
        endOfSentence = true;
    }

    void EndDialogue()
    {
        if(dialogues == null || dialogues.Count > 0)
        {
            Dialogue curDialogue = dialogues.Dequeue();
            nameText.text = curDialogue.dialogueData.displayName;
            sentences.Clear();
            foreach(Sentence sentence in curDialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            endOfSentence = true;
            DisplayNextSentence();
            return;
        }
        
        isOpen = false;

        GameManager.instance.ChangeState(GameStates.playing);
    }
}
