using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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

    public Text text;
    public GameObject canvas;

    bool gameOver;
    Rank[] ranks;

    void Start()
    {
        ranks = new Rank[players.Length];
        //find players...

        canvas.SetActive(false);
    }

    public void EndGame()
    {
        GetRanks();
        GetComponent<LoadSceneComponent>().LoadScene("GameOverScreen");

        canvas.SetActive(true);
        text.text = RanksToString();
        //Debug.Log(ranks[0].ToString());
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
            Debug.Log(ranks);
            ranks[rankTemp] = new Rank($"Player {b + 1}", (byte)(rankTemp + 1), players[b].Score);
        }
    }

    string RanksToString()
    {
        var sb = new StringBuilder();

        for (byte b = 0; b < players.Length; b++)
        {
            sb.AppendLine(ranks[b].ToString());
        }
        return sb.ToString();
    }
}
