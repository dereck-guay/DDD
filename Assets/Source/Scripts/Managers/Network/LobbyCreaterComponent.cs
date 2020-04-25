using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCreaterComponent : MonoBehaviourPunCallbacks
{

    public int multiplayerSceneIndex;

    public override void OnEnable() =>
        PhotonNetwork.AddCallbackTarget(this);

    public override void OnDisable() =>
        PhotonNetwork.AddCallbackTarget(this);

    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined");
        StartGame();
    }

    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting Game!");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
    }
}
