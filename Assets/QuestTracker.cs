using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestTracker : MonoBehaviour
{
    float appleCount;
    float trashCleaned;

    public bool collectedAllTrash;
    public bool collectedAllApple;
    public bool collectedAllRing;

    public bool appleQuestDone = false;
    public bool ringQuestDone = false;
    public bool trashQuestDone = false;


    
    public void GainApple()
    {
        appleCount++;
        if(appleCount = 5)
        {
            collectedAllApple = true;
        }
    }
    public void CleanTrash()
    {
        trashCleaned++;
        if(trashCleaned = 3)
        {
            collectedAllTrash = true;
        }
    }
    void Start()
    {
        appleCount = 0;
        trashCleaned = 0;
        collectedAllTrash = false;
    }
    public void GainRing()
    {
        collectedAllRing = true;
    }
    
}
