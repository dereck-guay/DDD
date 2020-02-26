﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapComponent : MonoBehaviour
{
    public int mapSize;

    Map mapModel;

    void Awake() => mapModel = new Map(mapSize);
    void Start() => Instantiate();

    public void Instantiate()
    {
        int posX, posY;
        GameObject room, trueRoom;

        for (int i = 0; i < mapModel.MapSize; i++)
        {
            posY = Room.Length * (i - mapModel.MapMiddle);

            for (int j = 0; j < mapModel.MapSize; j++)
            {
                posX = Room.Width * (j - mapModel.MapMiddle);

                if (mapModel.HasRoom(j, i))
                {
                    room = new GameObject("Room" + (i * mapModel.MapSize + j));
                    trueRoom = Instantiate(room, new Vector3(posX, 0, -posY), Quaternion.identity, transform); //(posY, 0, posX)
                    Destroy(room);

                    trueRoom.AddComponent<RoomComponent>().roomModel = mapModel[j, i];
                    trueRoom.GetComponent<RoomComponent>().Instantiate();
                }
            }
        }
    }
}