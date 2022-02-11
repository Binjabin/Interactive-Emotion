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
        Debug.Log(isApple);
        if(isApple == false && isRing == false && isTrash == false)
        {
            Debug.Log("eee");
            if(!dialogueSystem.inDialogue)
            {
                dialogueSystem.StartDialogue(GetComponent<Dialogue>());
            }
        }
        else
        {
            if(isApple)
            {
                Debug.Log("aaa");
                if(!isSpokenTo)
                {
                    dialogueSystem.StartDialogue(firstDialog);
                    FindObjectOfType<QuestTracker>().startedApple = true;
                }
                else if(FindObjectOfType<QuestTracker>().collectedAllApple)
                {
                    dialogueSystem.StartDialogue(doneDialog);
                    QuestTracker.appleQuestDone = true;
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
                    FindObjectOfType<QuestTracker>().startedTrash = true;
                }
                else if(FindObjectOfType<QuestTracker>().collectedAllTrash)
                {
                    dialogueSystem.StartDialogue(doneDialog);
                    QuestTracker.trashQuestDone = true;
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
                    FindObjectOfType<QuestTracker>().startedRing = true;
                }
                else if(FindObjectOfType<QuestTracker>().collectedAllRing)
                {
                    dialogueSystem.StartDialogue(doneDialog);
                    QuestTracker.ringQuestDone = true;
                }
                else if(isSpokenTo)
                {
                    dialogueSystem.StartDialogue(duringDialog);
                }
            }

        }
        isSpokenTo = true;

        
    }
    
}
