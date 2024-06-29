using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections.Generic;

public class TeamManager : MonoBehaviourPunCallbacks
{
    public Dictionary<int, string> playerTeams = new Dictionary<int, string>();

    public void JoinTeam(string teamName)
    {
        photonView.RPC("UpdateTeam", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, teamName);
    }

    [PunRPC]
    public void UpdateTeam(int playerID, string teamName)
    {
        if (playerTeams.ContainsKey(playerID))
        {
            playerTeams[playerID] = teamName;
        }
        else
        {
            playerTeams.Add(playerID, teamName);
        }

        Debug.Log($"Player {playerID} joined {teamName}");
    }
}