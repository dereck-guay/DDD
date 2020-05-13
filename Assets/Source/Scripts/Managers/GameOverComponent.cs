using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Rank
{
    string playerName;
    byte rank;
    int score;

    public Rank(string playerNameI, byte rankI, int scoreI)
    {
        playerName = playerNameI;
        rank = rankI;
        score = scoreI;
    }

    public override string ToString() => $"#{rank} {playerName} : {score}";
}

[RequireComponent(typeof(LoadSceneComponent))]
public class GameOverComponent : MonoBehaviour
{
    public PlayerMonoBehaviour[] players;
    bool gameOver;
    Rank[] ranks;

    void Start()
    {
        ranks = new Rank[players.Length];
        //find players...
        DontDestroyOnLoad(this);
    }

    public void EndGame()
    {
        GetRanks();
        GetComponent<LoadSceneComponent>().LoadScene("GameOverScreen");
        Debug.Log(ranks[0].ToString());
    }

    void GetRanks()
    {
        byte rankTemp;

        for (byte b = 0; b < players.Length; b++)
        {
            rankTemp = 0;

            for (byte c = 0; c < players.Length; c++)
            {
                if (players[b].Score < players[c].Score)
                    rankTemp++;

            }

            ranks[rankTemp] = new Rank($"Player {b + 1}", (byte)(rankTemp + 1), players[b].Score);
        }
    }
}
