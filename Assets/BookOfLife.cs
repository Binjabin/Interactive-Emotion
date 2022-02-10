using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BookOfLife : Interactible
{
    [SerializeField] AudioSource rumble;
    [SerializeField] GameObject blackOut;

    public override void Interact()
    {
        StartCoroutine(EnterGame());

    }

    IEnumerator EnterGame()
    {
        FindObjectOfType<PlayerLook>().Focus(FindObjectOfType<BookOfLife>().transform);
        float elapsedTime = 0f;
        float waitTime = 5f;
        rumble.Play();

        while (elapsedTime < waitTime)
        {
            Debug.Log(elapsedTime);
            FindObjectOfType<Camera>().fieldOfView = Mathf.Lerp(60f, 25f, (elapsedTime / waitTime));
            blackOut.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0f, 1f, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }      
        SceneManager.LoadScene(1);
        yield return null;
    }
}