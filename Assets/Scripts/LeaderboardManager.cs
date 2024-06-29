using Photon.Pun;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviourPun
{
    private Dictionary<int, int> playerScores = new Dictionary<int, int>();

    public void AddScore(int playerId, int score)
    {
        if (playerScores.ContainsKey(playerId))
        {
            playerScores[playerId] += score;
        }
        else
        {
            playerScores.Add(playerId, score);
        }

        photonView.RPC("UpdateLeaderboard", RpcTarget.All, playerId, playerScores[playerId]);
    }

    [PunRPC]
    void UpdateLeaderboard(int playerId, int totalScore)
    {
        playerScores[playerId] = totalScore;

        Debug.Log($"Player {playerId} now has {totalScore} points.");
    }

    public void DisplayLeaderboard()
    {
        var sortedScores = from entry in playerScores orderby entry.Value descending select entry;
        foreach (var entry in sortedScores)
        {
            Debug.Log($"Player {entry.Key} has {entry.Value} points.");
        }
    }
