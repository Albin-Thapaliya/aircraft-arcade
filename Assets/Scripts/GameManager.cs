using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player Plane")]
    [SerializeField] private GameObject playerPlanePrefab;
    [SerializeField] private Transform playerSpawnPoint;

    [Header("AI Plane Spawner")]
    [SerializeField] private AIPlaneSpawner aiPlaneSpawner;

    [Header("Power-Up Settings")]
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private Transform[] powerUpSpawnPoints;
    [SerializeField] private float powerUpSpawnInterval = 30f;

    private GameObject playerPlane;

    private void Start()
    {
        SpawnPlayer();
        aiPlaneSpawner.StartCoroutine("SpawnAIPlanes");
        InvokeRepeating(nameof(SpawnPowerUp), powerUpSpawnInterval, powerUpSpawnInterval);
    }

    public void SpawnPlayer()
    {
        if (playerPlane != null)
        {
            Destroy(playerPlane);
        }

        playerPlane = Instantiate(playerPlanePrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SpawnPowerUp()
    {
        Transform spawnPoint = powerUpSpawnPoints[Random.Range(0, powerUpSpawnPoints.Length)];
        GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        Instantiate(powerUpPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}