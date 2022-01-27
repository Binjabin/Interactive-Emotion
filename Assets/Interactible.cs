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

    // Update is called once per frame
    void Start()
    {
        cam = GameObject.FindWithTag("Camera");
        quest = FindObjectOfType<QuestTracker>();
    }
    void Update()
    {
        if(Vector3.Distance(cam.transform.position, transform.position) < interactionRange)
        {
           if(Input.GetKeyDown(KeyCode.E))
           {
               Interact();

           }
            
        }

    }
    public virtual void Interact()
    {
        Debug.Log("Interact");
    }

}
