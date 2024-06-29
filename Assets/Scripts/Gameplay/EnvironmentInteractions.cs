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

    public void ToggleInteractable(int index)
    {
        if (index >= 0 && index < interactables.Length)
        {
            interactables[index].SetActive(!interactables[index].activeSelf);
            Debug.Log($"Toggled {interactables[index].name}");

            if (interactables[index].activeSelf)
            {
                Debug.Log($"{interactables[index].name} is now active.");
            }
            else
            {
                Debug.Log($"{interactables[index].name} is now inactive.");
            }

            AudioSource audioSource = interactables[index].GetComponent<AudioSource>();

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    public void DisplayInteractables()
    {
        foreach (var obj in interactables)
        {
            Debug.Log(obj.name);
        }
    }

    public void SetInteractables(GameObject[] newInteractables)
    {
        interactables = newInteractables;
    }

    public GameObject[] GetInteractables()
    {
        return interactables;
    }

    public void SetInteractable(int index, GameObject obj)
    {
        if (index >= 0 && index < interactables.Length)
        {
            interactables[index] = obj;
        }
    }

    public GameObject GetInteractable(int index)
    {
        if (index >= 0 && index < interactables.Length)
        {
            return interactables[index];
        }

        return null;
    }

    public void AddInteractable(GameObject obj)
    {
        GameObject[] newInteractables = new GameObject[interactables.Length + 1];
        interactables.CopyTo(newInteractables, 0);
        newInteractables[newInteractables.Length - 1] = obj;
        interactables = newInteractables;
    }

    public void RemoveInteractable(int index)
    {
        if (index >= 0 && index < interactables.Length)
        {
            GameObject[] newInteractables = new GameObject[interactables.Length - 1];
            int j = 0;

            for (int i = 0; i < interactables.Length; i++)
            {
                if (i != index)
                {
                    newInteractables[j] = interactables[i];
                    j++;
                }
            }

            interactables = newInteractables;
        }
    }

    public void ClearInteractables()
    {
        interactables = new GameObject[0];
    }
}
