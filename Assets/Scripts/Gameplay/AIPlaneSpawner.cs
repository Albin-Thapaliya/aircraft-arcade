using UnityEngine;
using Photon.Pun;
using System.Collections;

public class AIPlaneSpawner : MonoBehaviour
{
    [Header("AI Plane Prefab")]
    [SerializeField] private GameObject aiPlanePrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Spawn Parameters")]
    [Tooltip("Number of AI planes to spawn")] public int numberOfPlanes = 5;
    [Tooltip("Time interval between spawns")] public float spawnInterval = 5f;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(SpawnAIPlanes());
        }
    }

    public IEnumerator SpawnAIPlanes()
    {
        for (int i = 0; i < numberOfPlanes; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            PhotonNetwork.Instantiate(aiPlanePrefab.name, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}