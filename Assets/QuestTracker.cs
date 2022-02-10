using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestTracker : MonoBehaviour
{
    float appleCount;
    float trashRemaining;

    bool collectedAllTrash;
    
    public void GainApple()
    {
        appleCount++;
    }
    public void CleanTrash()
    {
        trashRemaining -= 1;
        if(trashRemaining < 1)
        {
            collectedAllTrash = true;
        }
    }
    void Start()
    {
        appleCount = 0;
        trashRemaining = 5;
        collectedAllTrash = false;
    }
    public void OpenBook()
    {
        
    }
    
}
