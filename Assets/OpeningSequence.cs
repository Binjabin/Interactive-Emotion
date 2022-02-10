using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningSequence : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject splashText;
    [SerializeField] AudioClip poem1;
    [SerializeField] AudioClip poem2;
    [SerializeField] AudioClip poem3;
    [SerializeField] AudioClip poem4;
    [SerializeField] AudioClip poem5;
    [SerializeField] AudioClip intenseSoundtrack;
    [SerializeField] AudioClip calmSoundtrack;
    [SerializeField] AudioClip endingSoundtrack;
    [SerializeField] AudioSource poemPlayer;
    [SerializeField] AudioSource tunePlayer;
    [SerializeField] AudioSource rumblePlayer;
    [SerializeField] AudioSource lightningPlayer;

    CanvasGroup splashMaterial;
    [SerializeField] float fadeSpeed;
    [SerializeField] GameObject openingBlocker;
    [SerializeField] Transform initialFocus;


     
    IEnumerator Start()
    {
        FindObjectOfType<PlayerLook>().Focus(initialFocus);
        FindObjectOfType<PlayerController>().LockMovement();
        openingBlocker.SetActive(true);
        tunePlayer.clip = intenseSoundtrack;
        tunePlayer.Play();

        splashMaterial = splashText.GetComponent<CanvasGroup>();
        AudioSource poemPlayer = GetComponent<AudioSource>();
        splashMaterial.alpha = 0f;
        yield return new WaitForSeconds(3f);

        poemPlayer.clip = poem1;
        StartCoroutine(ColorFade(true));
        
        poemPlayer.Play();
        yield return new WaitForSeconds(poemPlayer.clip.length + 1);
        StartCoroutine(ColorFade(false));

        poemPlayer.clip = poem2;
        poemPlayer.Play();
        yield return new WaitForSeconds(poemPlayer.clip.length + 1);


        poemPlayer.clip = poem3;
        poemPlayer.Play();
        yield return new WaitForSeconds(poemPlayer.clip.length + 1);

        poemPlayer.clip = poem4;
        poemPlayer.Play();
        FindObjectOfType<DayNightCycle>().GoStormy();
        yield return new WaitForSeconds(poemPlayer.clip.length + 1);

        poemPlayer.clip = poem5;
        poemPlayer.Play();
        yield return new WaitForSeconds(poemPlayer.clip.length + 1);

        tunePlayer.clip = calmSoundtrack;
        tunePlayer.Play();

        FindObjectOfType<PlayerLook>().Unfocus();
        FindObjectOfType<PlayerController>().UnlockMovement();
    }

    // Update is called once per frame


    IEnumerator ColorFade(bool fadeIn)
    {
        // Lerp start value.
        float change = 0.0f;
         
         // Loop until lerp value is 1 (fully changed)
        while (change < 1.0f)
        {
            // Reduce change value by fadeSpeed amount.
            change += fadeSpeed * Time.deltaTime;
            if(fadeIn)
            {
                splashMaterial.alpha = change;
            }
            else
            {
                splashMaterial.alpha = 1 - change;
            }
            
             
            yield return null;
        }
        openingBlocker.SetActive(false);
        
    }

    public void EndGame()
    {
        StartCoroutine(End());
        Debug.Log("end");
    }

    IEnumerator End()
    {
        Debug.Log("end");
        rumblePlayer.Play();
        yield return new WaitForSeconds(rumblePlayer.clip.length - 1);
        rumblePlayer.Stop();
        lightningPlayer.Play();
        openingBlocker.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }

    public void EndTheme()
    {
        tunePlayer.clip = endingSoundtrack;
        tunePlayer.Play();

    }
    public void Storm()
    {
        tunePlayer.volume = 0.04f;
    }
    public void Unstorm()
    {
        tunePlayer.volume = 0.5f;
    }
}
