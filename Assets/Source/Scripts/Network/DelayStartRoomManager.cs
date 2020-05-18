using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStartRoomManager : MonoBehaviourPunCallbacks
{
    // Scene to be loaded when the room is joined
    public int waitingRoomSceneIndex; 

    // AddCallbackTarget makes function available to other scripts.
    public override void OnEnable() => PhotonNetwork.AddCallbackTarget(this);

    public override void OnDisable() => PhotonNetwork.RemoveCallbackTarget(this);

    // Should load into waiting room (Champ select).
    public override void OnJoinedRoom() => SceneManager.LoadScene(waitingRoomSceneIndex);
}
