using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int level;
    public int experience;

    public int ExperienceToNextLevel(int currentLevel)
    {
        return 1000 + (currentLevel * 200);
    }

    public bool AddExperience(int amount)
    {
        experience += amount;
        while (experience >= ExperienceToNextLevel(level))
        {
            experience -= ExperienceToNextLevel(level);
            level++;
            return true;
        }
        return false;
    }
}

public class ProgressionManager : MonoBehaviour
{
    public PlayerStats stats;

    public void GainExperience(int amount)
    {
        bool leveledUp = stats.AddExperience(amount);
        if (leveledUp)
        {
            Debug.Log("Level Up! New Level: " + stats.level);
        }
    }
}
