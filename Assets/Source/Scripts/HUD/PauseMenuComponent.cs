using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuComponent : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
<<<<<<< HEAD
    public GameObject audioMenu;
    public GameObject vsMenu;
    public GameObject controlsMenu;
    public GameObject characterSelctMenu;
    public GameObject resumeBtn;


    
=======
    public GameObject resunmeBtn;

>>>>>>> ca977158dae0d27f8cbb56efe67450a78aebb4fd
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
<<<<<<< HEAD
        audioMenu.SetActive(false);
        vsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        characterSelctMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        resumeBtn.transform.localScale = Vector3.one;
=======
        Time.timeScale = 1f;
        GameIsPaused = false;
        resunmeBtn.GetComponentInChildren<Button>().transform.localScale += new Vector3(-.1f, -.1f, -.1f);
>>>>>>> ca977158dae0d27f8cbb56efe67450a78aebb4fd
    }
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
