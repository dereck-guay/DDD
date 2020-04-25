using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManagerComponent : MonoBehaviourPunCallbacks
{
    private byte roomSize = 4;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom(); // Tries to join existing room.
        Debug.Log("Queued Up.");
    }


    public void CancelJoinRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("You left the room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room... /n Creating new room.");
        CreateRoom();
    }

    private void CreateRoom()
    {
        int newRoomId = Random.Range(0, 10000); // Creating a new room with the random id from 0 to 10000
        var roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = roomSize
        };

        PhotonNetwork.CreateRoom("Room #" + newRoomId, roomOptions);
        Debug.Log("Room #" + newRoomId);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room... Trying again!");
        CreateRoom();
    }
}
