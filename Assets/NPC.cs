using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactible
{
    DialogueSystem dialogueSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Interact()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        if(!dialogueSystem.inDialogue)
        {
            dialogueSystem.StartDialogue(GetComponent<Dialogue>());
        }
    }
}
