using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialogueUI;
    [SerializeField] GameObject otherUI;
    private Dialogue currentDialogue;
    private Interactible currentNPC;
    public bool inDialogue = false;

    public Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    void LateUpdate()
    {
        dialogueUI.SetActive(inDialogue);
        otherUI.SetActive(!inDialogue);
        if(inDialogue)
        {
            if(Input.GetButtonDown("Jump"))
            {
                DisplayNextSentence();
            }
        }
    }

    // Update is called once per frame
    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        sentences.Clear();
        currentDialogue = dialogue;
        currentNPC = dialogue.gameObject.GetComponent<Interactible>();
        inDialogue = true;
        FindObjectOfType<PlayerLook>().Focus(dialogue.transform);
        FindObjectOfType<PlayerController>().LockMovement();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        inDialogue = false;
        FindObjectOfType<PlayerLook>().Unfocus();
        FindObjectOfType<PlayerController>().UnlockMovement();
    }
}
