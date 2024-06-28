using UnityEngine;
using System.IO;

public static class FileManager
{
    private static string dataPath = Path.Combine(Application.persistentDataPath, "gameData.json");

    public static void SaveData(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, json);
        Debug.Log("Data saved to " + dataPath);
    }

    public static GameData LoadData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            return JsonUtility.FromJson<GameData>(json);
        }
        Debug.LogWarning("Save file not found in " + dataPath);
        return new GameData();
    }
}
