using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
    const string MapPath = "Prefabs\\MapPrefabs";
    readonly string[] DeadEnds = { "0001", "0010", "0100", "1000" };

    public Room roomModel;

    Object[] prefabs;
    public bool IsValidRespawnPoint { get; private set; }

    void Awake() => prefabs = Resources.LoadAll(MapPath);

    public void InstantiateRoom()
    {
        GameObject tile;

        for (int i = 0; i < Room.Length; i++)
            for (int j = 0; j < Room.Width; j++)
            {
                tile = (GameObject)Instantiate(prefabs[roomModel[j, i]], transform);
                tile.transform.localPosition = new Vector3(j - Room.Width / 2, 0, -i + Room.Length / 2);

                if (roomModel[j, i] == 0 && Random.value > 0.99f)
                    Instantiate((GameObject)prefabs[prefabs.Length - 1], tile.transform).transform.localPosition = Vector3.zero;
            }

        //Dead end => valid respawn point
        var shape = roomModel.GetShape();

        foreach (var s in DeadEnds)
            if (shape == s)
            {
                IsValidRespawnPoint = true;
                break;
            }
    }
}
