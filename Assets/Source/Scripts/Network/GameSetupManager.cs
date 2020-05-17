using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetupManager : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer() =>
        PhotonNetwork.Instantiate(Path.Combine("NetworkPrefabs", "NetworkPlayer"), Vector3.zero, Quaternion.identity);
}
