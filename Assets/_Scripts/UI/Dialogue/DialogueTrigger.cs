using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Dialogue dialogue;
    private void Awake()
    {
        dialogueManager = DialogueManager.instance;
    }
    public void TriggerDialogue()
    {
        if (dialogueManager != null) { 
            if (!dialogueManager.onDialogue)
            {
                dialogueManager.StartDialogue(dialogue);
            } else
            {
                dialogueManager.DisplayNextSentence();
            }
        }

    }
    public void StopDialog()
    {
        if (dialogueManager != null)
        {
            dialogueManager.EndDialogue();
        }
    }
}