﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneComponent : MonoBehaviour
{
    public GameObject managers;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));        
    }

    IEnumerator LoadAsynchronously (string sceneName)
    {
        Destroy(managers);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress * 100 + "%";

            yield return null;
        }
    }
}
