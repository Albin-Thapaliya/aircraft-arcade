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
    private int score = 0;
    private float difficultyMultiplier = 1.0f;

    void Start()
    {
        SpawnPlayer();
        InvokeRepeating("SpawnPowerUp", powerUpSpawnInterval, powerUpSpawnInterval);
        InvokeRepeating("IncreaseDifficulty", 60f, 60f);
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

    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Score: {score}");
    }

    private void IncreaseDifficulty()
    {
        difficultyMultiplier += 0.1f;
        aiPlaneSpawner.spawnInterval /= difficultyMultiplier;
        Debug.Log($"Difficulty increased: {difficultyMultiplier}");
    }
}