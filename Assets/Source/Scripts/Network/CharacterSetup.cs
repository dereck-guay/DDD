using System.Collections.Generic;
using Photon.Pun;
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
            PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedCharacter);
    }

    [PunRPC]
    private void RPC_AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;
        myCharacter = Instantiate(PlayerInfo.PI.allCharacters[whichCharacter].gameObject, transform.position, transform.rotation, transform);
        myCharacter.SetActive(true);
        var pV = myCharacter.AddComponent<PhotonView>();
        var pTV  = myCharacter.AddComponent<PhotonTransformView>();
        pV.ObservedComponents = new List<Component>();
        pV.ObservedComponents.Add(pTV);
    }
}
