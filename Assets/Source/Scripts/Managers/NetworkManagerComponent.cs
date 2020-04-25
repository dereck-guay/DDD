using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Concepts tiré de la série Youtube : https://www.youtube.com/watch?v=02P_mrszvzY
public class NetworkManagerComponent : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to: " + PhotonNetwork.CloudRegion);
    }
}
