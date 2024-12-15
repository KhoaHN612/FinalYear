using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Dialogue dialogue;
    private void Start()
    {
        dialogueManager = DialogueManager.instance;
    }
    public void TriggerDialogue()
    {
        //Debug.Log("Trigger Dialogue");
        if (dialogueManager != null) { 
            //Debug.Log("Dialogue Manager is not null");
            if (!dialogueManager.onDialogue)
            {
                //Debug.Log("Start Dialogue");
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