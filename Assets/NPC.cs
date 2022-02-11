using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactible
{
    DialogueSystem dialogueSystem;
    [SerializeField] bool isApple;
    [SerializeField] bool isTrash;
    [SerializeField] bool isRing;
    bool isSpokenTo;

    [SerializeField] Dialogue firstDialog;
    [SerializeField] Dialogue duringDialog;
    [SerializeField] Dialogue doneDialog;

    // Start is called before the first frame update
    void Start()
    {
        isSpokenTo = false;
    }

    // Update is called once per frame
    public override void Interact()
    {
        
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        if(!(isApple || isRing || isTrash))
        {
            if(!dialogueSystem.inDialogue)
            {
                dialogueSystem.StartDialogue(GetComponent<Dialogue>());
            }
        }
        else
        {
            if(isApple)
            {
                if(!isSpokenTo)
                {
                    dialogueSystem.StartDialogue(firstDialog);
                }
                else if(FindObjectOfType<QuestTracker>().collectedAllApple)
                {
                    dialogueSystem.StartDialogue(doneDialog);
                    FindObjectOfType<QuestTracker>().appleQuestDone = true;
                }
                else if(isSpokenTo)
                {
                    dialogueSystem.StartDialogue(duringDialog);
                }
            }
            else if(isTrash)
            {
                if(!isSpokenTo)
                {
                    dialogueSystem.StartDialogue(firstDialog);
                }
                else if(FindObjectOfType<QuestTracker>().collectedAllTrash)
                {
                    dialogueSystem.StartDialogue(doneDialog);
                    FindObjectOfType<QuestTracker>().trashQuestDone = true;
                }
                else if(isSpokenTo)
                {
                    dialogueSystem.StartDialogue(duringDialog);
                }
            }
            else if(isRing)
            {
                if(!isSpokenTo)
                {
                    dialogueSystem.StartDialogue(firstDialog);
                }
                else if(FindObjectOfType<QuestTracker>().collectedAllRing)
                {
                    dialogueSystem.StartDialogue(doneDialog);
                    FindObjectOfType<QuestTracker>().ringQuestDone = true;
                }
                else if(isSpokenTo)
                {
                    dialogueSystem.StartDialogue(duringDialog);
                }
            }

        }

        
    }
    isSpokenTo = true;
}
