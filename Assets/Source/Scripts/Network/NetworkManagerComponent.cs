using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerComponent : MonoBehaviourPunCallbacks
{
    // Instance accessible partout dans le code NetworkManagerComponent.instance;
    public static NetworkManagerComponent instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            gameObject.SetActive(false);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Garde le gameObject quand on change de scene.
        }
    }

    private void Start() =>
        PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
}
