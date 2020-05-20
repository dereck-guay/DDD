using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterSetup : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myCharacter;
    public int characterValue;

    void Start()
    {
        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            AddCharacter(PlayerInfo.PI.mySelectedCharacter);
            PV.RPC("RPC_AddCharacter", RpcTarget.OthersBuffered, PlayerInfo.PI.mySelectedCharacter, PV.Owner);
        }
    }


    /// <summary>
    ///     Ajoute le character pour le client (sender).
    /// </summary>
    /// <param name="whichCharacter"></param>
    private void AddCharacter(int whichCharacter)
    {
        SetCharacter(whichCharacter);

        // Set main camera to player
        myCharacter.GetComponent<PlayerMonoBehaviour>().camera = Camera.main;
    }


    /// <summary>
    ///     Ajoute le selected Character aux autres clients.
    /// </summary>
    /// <param name="whichCharacter"></param>
    /// <param name="Owner"></param>
    [PunRPC]
    private void RPC_AddCharacter(int whichCharacter, Player Owner)
    {
        SetCharacter(whichCharacter);
        var pV = myCharacter.GetComponent<PhotonView>();
        pV.TransferOwnership(Owner);

        // Turn off CharacterHUD pour les autres joueurs.
        myCharacter.transform.GetChild(3).gameObject.SetActive(false);
    }

    private void SetCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;
        var characterName = PlayerInfo.PI.allCharacters[whichCharacter].gameObject.name;
        myCharacter = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "ClassPrefabs", characterName), transform.position, transform.rotation, 1);
    }
}
