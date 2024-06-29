using UnityEngine;
using Photon.Pun;
using System.Collections;

public class GameEventManager : MonoBehaviourPun
{
    public float eventInterval = 60.0f;
    private float nextEventTime;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            nextEventTime = Time.time + eventInterval;
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && Time.time >= nextEventTime)
        {
            TriggerRandomEvent();
            nextEventTime = Time.time + eventInterval;
        }
    }

    void TriggerRandomEvent()
    {
        int eventCode = Random.Range(0, 3);
        photonView.RPC("ExecuteEvent", RpcTarget.All, eventCode);
    }

    [PunRPC]
    void ExecuteEvent(int eventCode)
    {
        switch (eventCode)
        {
            case 0:
                Debug.Log("Meteor Shower Event Triggered!");
                break;
            case 1:
                Debug.Log("Health Boost Event Triggered!");
                break;
            case 2:
                Debug.Log("Speed Increase Event Triggered!");
                break;
            default:
                Debug.Log("Unknown Event");
                break;
        }
    }
}