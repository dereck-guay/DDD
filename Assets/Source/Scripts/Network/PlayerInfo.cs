using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PlayerInfo PI;
    public int currentScene;
    public int multiplayerSceneIndex;
    public int mySelectedCharacter;
    public GameObject[] allCharacters;

    private PhotonView PV;



    private void Awake()
    {
        if (PI == null)
            PI = this;
        else if (PlayerInfo.PI != this)
        {
            Destroy(PlayerInfo.PI.gameObject);
            PlayerInfo.PI = this;
        }

        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
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

    #region PhotonCallbacks

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    #endregion

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == multiplayerSceneIndex)
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        Debug.Log("Instantiating a new NetworkPlayer");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Network", "NetworkPlayer"), transform.position, Quaternion.identity, 0);
    }
}
