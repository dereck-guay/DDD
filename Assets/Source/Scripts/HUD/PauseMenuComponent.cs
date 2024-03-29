﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuComponent : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject audioMenu;
    public GameObject vsMenu;
    public CanvasGroup controlsMenu;
    public GameObject characterSelctMenu;
    public Button[] Buttons;

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
        controlsMenu.alpha = 0;
        controlsMenu.blocksRaycasts = false;
        characterSelctMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        foreach (Button btn in Buttons)
            btn.transform.localScale = Vector3.one;
    }
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        foreach (Button btn in Buttons)
            btn.interactable = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void DisableButtons()
    {
        foreach (Button btn in Buttons)
            btn.interactable = false;
    }

    public void EnableButtons()
    {
        foreach (Button btn in Buttons)
            btn.interactable = true;
    }
}
