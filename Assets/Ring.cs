using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Interactible
{
    public override void Interact()
    {
        quest = FindObjectOfType<QuestTracker>();
        quest.GainRing();
        gameObject.SetActive(false);
    }
}
