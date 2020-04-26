using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectComponent : MonoBehaviour
{
    public GameObject[] characterList;
    public Button[] characterSelect;
    public GameObject pauseMenu;
    public GameObject characterSelectMenu;
    private int index;
    private Vector3 lastPos;

    void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");

        for (int i = 0; i < characterSelect.Length; i++)
        {
            int i2 = i;
            characterSelect[i].onClick.AddListener(() => Select(i2));
        }

        foreach (GameObject character in characterList)
            character.SetActive(false);

        if (characterList[index])
            characterList[index].SetActive(true);
    }

    private void Update()
    {
        for (int i = 0; i < characterSelect.Length; i++)
        {
            int counter = i;
            characterSelect[i].onClick.AddListener(() => Switch(counter));
        }
        lastPos = characterList[index].transform.position;
    }

    public void Select(int index)
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
    }

    public void Switch(int newIndex)
    {
        characterSelect[newIndex].transform.localScale = Vector3.one;
        PauseMenuComponent.GameIsPaused = false;
        pauseMenu.SetActive(false);
        characterSelectMenu.SetActive(false);

        Time.timeScale = 1f;

        if (pauseMenu.activeInHierarchy == false)
        {
            characterList[index].SetActive(false);
            characterList[newIndex].SetActive(true);
            characterList[newIndex].transform.position = lastPos;
        }
    }
}
