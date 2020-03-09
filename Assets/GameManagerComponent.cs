using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerComponent : MonoBehaviour
{
    [Range(0, 120)]
    public int framerate;
    public int projectileLayer;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = framerate;

        Physics.IgnoreLayerCollision(projectileLayer, projectileLayer);
    }
}
