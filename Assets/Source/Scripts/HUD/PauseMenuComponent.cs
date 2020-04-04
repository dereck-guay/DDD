using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuComponent : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject audioMenu;
    public GameObject vsMenu;
    public GameObject controlsMenu;
    public GameObject characterSelctMenu;
    public Button resumeBtn;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        audioMenu.SetActive(false);
        vsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        characterSelctMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        resumeBtn.transform.localScale = Vector3.one;
    }
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
