using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MatchmakingController : MonoBehaviourPunCallbacks
{
    private const string gameVersion = "1.0";
    private const int maxPlayersPerRoom = 4;

    void Start()
    {
        ConnectToPhoton();
    }

    void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        JoinRandomRoom();
    }

    void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No random room available, creating a new room");
        CreateRoom();
    }

    void CreateRoom()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = maxPlayersPerRoom };
        PhotonNetwork.CreateRoom(null, options);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Player has joined the room");
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount == maxPlayersPerRoom)
        {
            Debug.Log("The room is full, starting the game...");
            PhotonNetwork.LoadLevel("Flight");
        }
        else
        {
            Debug.Log("Waiting for more players...");
        }
    }

    public void StartMatchmaking()
    {
        ConnectToPhoton();
    }
}
