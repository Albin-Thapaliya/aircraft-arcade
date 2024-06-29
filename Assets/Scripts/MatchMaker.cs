using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MatchMaker : MonoBehaviourPunCallbacks
{
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No rooms to join, creating a new room");
        CreateRoom();
    }

    void CreateRoom()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.CreateRoom(null, options);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("First player in the room, waiting for others...");
        }
        else
        {
            Debug.Log("Ready to play!");
        }
    }
}