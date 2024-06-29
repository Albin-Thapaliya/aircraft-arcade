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

            Debug.Log($"Sent RPC to all clients to interact with {interactables[index].name}");
        }
    }

    [PunRPC]
    void PerformInteraction(int index)
    {
        GameObject obj = interactables[index];
        obj.SetActive(!obj.activeSelf);
        Debug.Log($"Interacted with {obj.name}");

        if (obj.activeSelf)
        {
            Debug.Log($"{obj.name} is now active.");
        }
        else
        {
            Debug.Log($"{obj.name} is now inactive.");
        }

        AudioSource audioSource = obj.GetComponent<AudioSource>();

        if (audioSource != null)
        {

            audioSource.Play();

        }
    }
}
