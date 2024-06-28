using System.Collections;
using UnityEngine;

public class AIPlaneSpawner : MonoBehaviour
{
    [Header("AI Plane Prefab")]
    [SerializeField] private GameObject aiPlanePrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Spawn Parameters")]
    public int numberOfPlanes = 5;
    public float spawnInterval = 5f;

    private void Start()
    {
        StartCoroutine(SpawnAIPlanes());
    }

    private IEnumerator SpawnAIPlanes()
    {
        for (int i = 0; i < numberOfPlanes; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject aiPlane = Instantiate(aiPlanePrefab, spawnPoint.position, spawnPoint.rotation);

            Plane planeComponent = aiPlane.GetComponent<Plane>();
            if (planeComponent != null)
            {
                planeComponent.speed = Random.Range(30f, 70f);
                planeComponent.fireRate = Random.Range(0.5f, 2f);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}