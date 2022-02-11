using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestTracker : MonoBehaviour
{
    float appleCount;
    float trashCleaned;

    public bool collectedAllTrash = false;
    public bool collectedAllApple = false;
    public bool collectedAllRing = false;

    public bool startedTrash = false;
    public bool startedApple = false;
    public bool startedRing = false;

    public static bool appleQuestDone = false;
    public static bool ringQuestDone = false;
    public static bool trashQuestDone = false;

    [SerializeField] TextMeshProUGUI appleText;
    [SerializeField] TextMeshProUGUI trashText;
    [SerializeField] TextMeshProUGUI ringText;

    [SerializeField] GameObject appleGlobe;
    [SerializeField] GameObject trashGlobe;
    [SerializeField] GameObject ringGlobe;
    [SerializeField] bool gameScene;

    private void Update()
    {
        if(gameScene)
        {
            if(startedTrash == true && collectedAllTrash == false)
            {
                trashText.text = "Clean up 3 bags of rubbish (" + trashCleaned + "/3)";
            }
            else if(startedTrash == true && collectedAllTrash == true)
            {
                trashText.text = "Speak to Imran again!";
            }
            else if(trashQuestDone == true)
            {
                trashText.text =  "Trash cleaned up!";
            }
            else
            {
                trashText.text =  "Speak to Imran";
            }

            if(startedApple == true && collectedAllApple == false)
            {
                appleText.text = "Collect 5 apples for Granny's pie. (" + appleCount + "/5)";
            }
            else if(startedApple == true && collectedAllApple == true)
            {
                appleText.text = "Bring the 5 apples back to granny.";
            }
            else if(appleQuestDone == true)
            {
                appleText.text =  "Apple's given!";
            }
            else
            {
                appleText.text =  "Speak to Granny";
            }

            if(startedRing == true && collectedAllRing == false)
            {
                ringText.text = "Find Dr Morton's ring";
            }
            else if(startedRing == true && collectedAllApple == true)
            {
                ringText.text = "Return the ring to Dr Morton";
            }
            else if(ringQuestDone == true)
            {
                ringText.text =  "Ring returned!";
            }
            else
            {
                ringText.text =  "Speak to Dr Morton";
            }
        }
        else
        {
            ringGlobe.SetActive(ringQuestDone);
            trashGlobe.SetActive(trashQuestDone);
            appleGlobe.SetActive(appleQuestDone);
        }
        
        
    }
    
    public void GainApple()
    {
        appleCount++;
        if(appleCount == 5)
        {
            collectedAllApple = true;
        }
    }
    public void CleanTrash()
    {
        trashCleaned++;
        if(trashCleaned == 3)
        {
            collectedAllTrash = true;
        }
    }
    void Start()
    {
        appleCount = 0;
        trashCleaned = 0;
        collectedAllTrash = false;

        startedTrash = false;
        startedApple = false;
        startedRing = false;
    }
    public void GainRing()
    {
        collectedAllRing = true;
    }
    
}
