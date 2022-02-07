using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookOfLife : Interactible
{

    public override void Interact()
    {
        quest = FindObjectOfType<QuestTracker>();
        quest.OpenBook();
    }
}