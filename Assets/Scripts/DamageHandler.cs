using UnityEngine;
using Photon.Pun;

public class DamageHandler : MonoBehaviourPun
{
    public int health = 100;

    public void TakeDamage(int amount, PhotonPlayer source)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
            if (photonView.IsMine)
            {
                photonView.RPC("ReportDeath", RpcTarget.All, source);
            }
        }
    }

    void Die()
    {
        Debug.Log($"{PhotonNetwork.Nickname} died");
        
        if (photonView.IsMine)
        {
            FindObjectOfType<LeaderboardManager>().DisplayLeaderboard();
        }

        health = 100;

        FindObjectOfType<RespawnManager>().Respawn(gameObject);
        GetComponent<AudioSource>().Play();
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Shooting>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Camera>().enabled = false;
        GetComponent<AudioListener>().enabled = false;
        GetComponent<Light>().enabled = false;
        GetComponent<Light>().enabled = false;
    }

    [PunRPC]
    void ReportDeath(PhotonPlayer killer)
    {
        Debug.Log($"{killer.NickName} killed {PhotonNetwork.Nickname}");

        killer.AddScore(1);
        FindObjectOfType<LeaderboardManager>().AddScore(killer, 1);
        FindObjectOfType<RespawnManager>().Respawn(gameObject);
        GetComponent<AudioSource>().Play();
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Shooting>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Camera>().enabled = false;
        GetComponent<AudioListener>().enabled = false;
        GetComponent<Light>().enabled = false;
    }
}
