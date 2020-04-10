using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettingsComponent : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown graphicsDropdown;
    public Dropdown vsyncDropdown;
    public Slider fpsSlider;
    Resolution[] resolutions;
    
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        resolutionDropdown.RefreshShownValue();

        graphicsDropdown.value = PlayerPrefs.GetInt("Graphics");
        graphicsDropdown.RefreshShownValue();

        vsyncDropdown.value = PlayerPrefs.GetInt("Vsync");
        vsyncDropdown.RefreshShownValue();

        fpsSlider.value = PlayerPrefs.GetInt("FPS");
    }

    private void Update()
    {
        SetFPS((int)fpsSlider.value);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("Graphics", graphicsDropdown.value);
        PlayerPrefs.SetInt("Vsync", vsyncDropdown.value);
        PlayerPrefs.SetInt("FPS", (int)fpsSlider.value);
        PlayerPrefs.Save();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFPS(int val)
    {
        Application.targetFrameRate = val;
        Text FPS = fpsSlider.GetComponentInChildren<Text>();
        FPS.text = val.ToString();
    }

    public void SetVsync(int vsyncIndex)
    {
        QualitySettings.vSyncCount = vsyncIndex;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
