using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Destroying duplicate DialogueManager");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public UnityEvent onEnter, onExit;

    public Animator animator;

    private Queue<string> sentences;

    public bool onDialogue = false;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        onDialogue = true;

        onEnter?.Invoke();

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        onExit?.Invoke();
        animator.SetBool("isOpen", false);
        onDialogue = false;
    }

}