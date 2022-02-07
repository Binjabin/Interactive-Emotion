using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    bool isPaused;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject nightPauseMenu;
    [SerializeField] GameObject dayPauseMenu;
    float timeSincePause;
    float timeSinceUnpause;
    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(isPaused);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            if(isPaused)
            {
                timeSincePause = 0f;
            }
            else
            {
                timeSinceUnpause = 0f;
            }
        }
        if(isPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None; 
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked; 
        }
        if(FindObjectOfType<DayNightCycle>().time < 0.05f || FindObjectOfType<DayNightCycle>().time > 0.95f)
        {
            nightPauseMenu.SetActive(true);
            dayPauseMenu.SetActive(false);
        }
        else
        {
            nightPauseMenu.SetActive(false);
            dayPauseMenu.SetActive(true);
        }
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

}
