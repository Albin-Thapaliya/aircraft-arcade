using Photon.Pun;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnRate = 10f;
    private float nextSpawnTime;

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && Time.time >= nextSpawnTime)
        {
            SpawnPowerUp();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnPowerUp()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        PhotonNetwork.Instantiate(powerUpPrefab.name, spawnPosition, Quaternion.identity);
    }
}