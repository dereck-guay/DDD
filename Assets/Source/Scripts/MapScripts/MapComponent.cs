using System.Collections;
using System.Collections.Generic;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;

public class MapComponent : MonoBehaviour
{
    public int mapSize;
    public GameObject floorPlane;
    public NavMeshSurface[] navMeshSurfaces;
    public RespawnManagerComponent respawnManager;

    Map mapModel;

    void Awake()
    {
        mapModel = new Map(mapSize);
    }

    void Start()
    {
        //Scales the floor to the side of the map.
        float floorScaleFactor = (2 * mapSize + 3) / 10f;
        floorPlane.transform.localScale = new Vector3(Room.Width, 0, Room.Length) * floorScaleFactor;

        InstantiateMap();

        foreach (var s in navMeshSurfaces)
            s.BuildNavMesh();
    }

    public void InstantiateMap()
    {
        int posX, posY;
        GameObject room, trueRoom;
        RoomComponent currentRoom;

        for (int i = 0; i < mapModel.MapSize; i++)
        {
            posY = Room.Length * (i - mapModel.MapMiddle);

            for (int j = 0; j < mapModel.MapSize; j++)
            {
                posX = Room.Width * (j - mapModel.MapMiddle);

                if (mapModel.HasRoom(j, i))
                {
                    room = new GameObject("Room" + (i * mapModel.MapSize + j));
                    trueRoom = Instantiate(room, new Vector3(posX, 0, -posY), Quaternion.identity, transform);
                    Destroy(room);

                    currentRoom = trueRoom.AddComponent<RoomComponent>();

                    currentRoom.roomModel = mapModel[j, i];
                    currentRoom.InstantiateRoom();

                    if (currentRoom.IsValidRespawnPoint)
                        respawnManager.AddRespawnPoint(currentRoom.transform.position);
                }
            }
        }
    }
}