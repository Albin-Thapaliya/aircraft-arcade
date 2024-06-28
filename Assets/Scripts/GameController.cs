public class GameController : MonoBehaviour
{
    private GameData gameData = new GameData();

    void Start()
    {
        LoadGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        gameData.score = 100;
        gameData.playerHealth = 75.0f;
        gameData.playerFuel = 50.0f;
        FileManager.SaveData(gameData);
    }

    public void LoadGame()
    {
        gameData = FileManager.LoadData();
        Debug.Log("Game Loaded: Score = " + gameData.score);
    }
}