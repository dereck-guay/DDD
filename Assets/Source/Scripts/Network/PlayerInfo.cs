using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;
    public int mySelectedCharacter;
    public GameObject[] allCharacters;

    private void OnEnable()
    {
        if (PI == null)
            PI = this;
        else if (PlayerInfo.PI != this)
        {
            Destroy(PlayerInfo.PI.gameObject);
            PlayerInfo.PI = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("myCharacter"))
            mySelectedCharacter = PlayerPrefs.GetInt("myCharacter");
        else
        {
            mySelectedCharacter = 0;
            PlayerPrefs.SetInt("myCharacter", mySelectedCharacter);
        }
    }
}
