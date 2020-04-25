using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NetworkPlayerManager : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();
    }
    
    private void CreatePlayer()
    {
        Debug.Log("Player added ton room.");

        // Ajoute l'objet qui représente un joueur connecter dans le network.
        // Photon s'occupera de l'ajouter uniquement une fois par client.
        PhotonNetwork.Instantiate(
            Path.Combine("PhotonPrefabs", "PhotonPlayer"),
            Vector3.zero,
            Quaternion.identity
        );
    }
}
