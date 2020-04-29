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
    public override void OnConnectedToMaster() =>
        Debug.Log("Connected to: " + PhotonNetwork.CloudRegion);

    // Create Room.
    public void CreateRoom(string roomName) => PhotonNetwork.CreateRoom(roomName);
    public override void OnCreatedRoom() => Debug.Log("You create a room.");
    public override void OnCreateRoomFailed(short returnCode, string message) => Debug.Log(message);

    // Join Room.
    public void JoinRoom(string roomName) => PhotonNetwork.JoinRoom(roomName);
    public void CancelJoinRoom() => PhotonNetwork.LeaveRoom();
    public override void OnJoinedRoom() => Debug.Log("You joined the room");
    public override void OnJoinRoomFailed(short returnCode, string message) => Debug.Log(message);

    // Change de scene au niveau du serveur.
    public void ChangeScene(string sceneName)
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(sceneName);
    }
        
}
