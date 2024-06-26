using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPlanePrefab;
    public Transform playerSpawnPoint;
    public AIPlaneSpawner aiPlaneSpawner;
    public GameObject[] powerUpPrefabs;
    public Transform[] powerUpSpawnPoints;
    public float powerUpSpawnInterval = 30f;

    private GameObject playerPlane;

    void Start()
    {
        SpawnPlayer();
        // StartCoroutine(aiPlaneSpawner.SpawnAIPlanes());
        InvokeRepeating("SpawnPowerUp", powerUpSpawnInterval, powerUpSpawnInterval);
    }

    void SpawnPlayer()
    {
        if (playerPlane != null)
        {
            Destroy(playerPlane);
        }

        playerPlane = Instantiate(playerPlanePrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SpawnPowerUp()
    {
        Transform spawnPoint = powerUpSpawnPoints[Random.Range(0, powerUpSpawnPoints.Length)];
        GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        Instantiate(powerUpPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}