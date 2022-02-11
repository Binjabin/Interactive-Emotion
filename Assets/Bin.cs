using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : Interactible
{
    public override void Interact()
    {
        quest = FindObjectOfType<QuestTracker>();
        quest.CleanTrash();
        gameObject.SetActive(false);
    }
}
