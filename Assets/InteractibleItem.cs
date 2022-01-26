using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleItem : MonoBehaviour
{
    public float interactionRange = 2f;
    [SerializeField] GameObject cam;
    [SerializeField] InteractibleItem item;
    [SerializeField] QuestTracker quest;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] bool isApple;
    [SerializeField] bool isRubbish;
    [SerializeField] bool isNPC;
    float appleCount;
    bool inConveration;

    // Update is called once per frame
    void Start()
    {
        cam = GameObject.FindWithTag("Camera");
        item = GetComponent<InteractibleItem>();
    }
    void Update()
    {
        if(Vector3.Distance(cam.transform.position, transform.position) < interactionRange)
        {
           if(Input.GetKeyDown(KeyCode.E))
           {
               if(isNPC)
               {
                    if(inConveration)
                    {
                        FindObjectOfType<DialogueSystem>().DisplayNextSentence();
                    }
                    else
                    {
                        inConveration = true;
                        FindObjectOfType<DialogueSystem>().StartDialogue(GetComponent<Dialogue>());
                    }
                   
               }
               else
               {
                    inventory.AddItem(item);
                    gameObject.SetActive(false);
                    if(isApple)
                    {
                        quest.GainApple();
                    }
                    if(isRubbish)
                    {
                        quest.CleanTrash();
                    }
               }

           }
            
        }

    }
    public void EndDialogue()
    {
        inConveration = false;
    }
}
