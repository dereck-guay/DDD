using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class DelayStartLobbyManagerComponent : MonoBehaviourPunCallbacks
{
    public GameObject delayStartButton;
    public GameObject delayCancelButton;
    public byte roomSize;

    public override void OnConnectedToMaster()
    {
        // Every clients loads the same scene.
        PhotonNetwork.AutomaticallySyncScene = true; 
        delayStartButton.SetActive(true);
    }

    public void DelayStart()
    {
        delayStartButton.SetActive(false);
        delayCancelButton.SetActive(true);

        // Tries to join a room.
        PhotonNetwork.JoinRandomRoom();
    }

    public void DelayCancel()
    {
        delayCancelButton.SetActive(false);
        delayStartButton.SetActive(true);

        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message) => CreateRoom();

    public override void OnCreateRoomFailed(short returnCode, string message) => CreateRoom(); // Probablement juste le meme roomName.

    private void CreateRoom()
    {
        // Creates a room.
        PhotonNetwork.CreateRoom(
            "Room_" + Random.Range(0, 1000),
            new RoomOptions()
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = roomSize
            }
        );
    }
}
