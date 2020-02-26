using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerCompoenent : MonoBehaviour
{
    [Range(0, 120)]
    public int framerate;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = framerate;
    }
}
