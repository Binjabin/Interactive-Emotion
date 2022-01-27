using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public float interactionRange = 2f;
    GameObject cam;
    public QuestTracker quest;
    float appleCount;
    
    public MonoBehaviour itemScript;


    public virtual void Interact()
    {
        Debug.Log("Interact");
    }

}
