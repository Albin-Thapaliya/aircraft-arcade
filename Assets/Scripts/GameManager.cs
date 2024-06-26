using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player Plane")]
    [SerializeField] private GameObject playerPlanePrefab;
    [SerializeField] private Transform playerSpawnPoint;

    private GameObject playerPlane;

    private void Start()
    {
        SpawnPlayer();
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
}