using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class DelayStartLobbyManagerComponent : MonoBehaviourPunCallbacks
{
    public GameObject playingOptions;
    public GameObject delayCancelButton;
    public GameObject backButton;
    public byte roomSize;

    public void DelayStartPvPvE()
    {
        playingOptions.SetActive(false);
        delayCancelButton.SetActive(true);

        PhotonNetwork.JoinRandomRoom();
    }

    public void DelayCancel()
    {
        delayCancelButton.SetActive(false);
        playingOptions.SetActive(true);

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
