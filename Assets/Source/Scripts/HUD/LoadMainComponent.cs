using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainComponent : MonoBehaviour
{
    public void LoadMain(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
