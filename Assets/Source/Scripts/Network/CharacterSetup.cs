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

    private void AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;
        var characterName = PlayerInfo.PI.allCharacters[whichCharacter].gameObject.name;
        myCharacter = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "ClassPrefabs", characterName), transform.position, transform.rotation, 1);
        var pV = myCharacter.GetComponent<PhotonView>();
        var pTV = myCharacter.AddComponent<PhotonTransformView>();
        pV.ObservedComponents = new List<Component>();
        pV.ObservedComponents.Add(pTV);
    }

    [PunRPC]
    private void RPC_AddCharacter(int whichCharacter, Player Owner)
    {
        characterValue = whichCharacter;
        var characterName = PlayerInfo.PI.allCharacters[whichCharacter].gameObject.name;
        myCharacter = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "ClassPrefabs", characterName), transform.position, transform.rotation, 1);
        var pV = myCharacter.GetComponent<PhotonView>();

        pV.TransferOwnership(Owner);

        var pTV  = myCharacter.AddComponent<PhotonTransformView>();
        pV.ObservedComponents = new List<Component>();
        pV.ObservedComponents.Add(pTV);
    }
}
