using UnityEngine;
using Photon.Pun;

public class EnvironmentInteractions : MonoBehaviourPun
{
    public GameObject[] interactables;

    public void ActivateInteraction(int index)
    {
        if (PhotonNetwork.IsMasterClient && index >= 0 && index < interactables.Length)
        {
            photonView.RPC("PerformInteraction", RpcTarget.All, index);
        }
    }

    [PunRPC]
    void PerformInteraction(int index)
    {
        GameObject obj = interactables[index];
        obj.SetActive(!obj.activeSelf);
        Debug.Log($"Interacted with {obj.name}");
    }
}
