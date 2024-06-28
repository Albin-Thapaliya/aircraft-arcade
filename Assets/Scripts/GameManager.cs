using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] planePrefabs;
    public Transform playerSpawnPoint;
    public Transform[] powerUpSpawnPoints;
    public GameObject[] powerUpPrefabs;
    public float powerUpSpawnInterval = 5f;
    public AIPlaneSpawner aiPlaneSpawner;

    private GameObject playerPlane;

    void Start()
    {
        SpawnPlayerPlane();
        StartCoroutine(aiPlaneSpawner.SpawnAIPlanes());
        InvokeRepeating("SpawnPowerUp", powerUpSpawnInterval, powerUpSpawnInterval);
    }

    private void SpawnPlayerPlane()
    {
        if (playerPlane != null)
        {
            Destroy(playerPlane);
        }
        int selectedPlaneIndex = PlayerPrefs.GetInt("SelectedPlaneIndex", 0);
        if (planePrefabs != null && selectedPlaneIndex < planePrefabs.Length)
        {
            playerPlane = Instantiate(planePrefabs[selectedPlaneIndex], playerSpawnPoint.position, playerSpawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Invalid plane selection or planePrefabs not properly assigned in the Inspector.");
        }
    }

    private void SpawnPowerUp()
    {
        Transform spawnPoint = powerUpSpawnPoints[Random.Range(0, powerUpSpawnPoints.Length)];
        GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        Instantiate(powerUpPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}