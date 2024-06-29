using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomPropertiesManager : MonoBehaviourPun
{
    public void SetRoomProperties(string mapType, int maxScore)
    {
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable
        {
            { "map", mapType },
            { "scoreLimit", maxScore }
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("map"))
        {
            Debug.Log("Map type changed to: " + propertiesThatChanged["map"]);
        }
        if (propertiesThatChanged.ContainsKey("scoreLimit"))
        {
            Debug.Log("Score limit changed to: " + propertiesThatChanged["scoreLimit"]);
        }
    }
}