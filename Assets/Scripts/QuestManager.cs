using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private List<Quest> activeQuests = new List<Quest>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AddQuest(new Quest("Find the Lost Sword", "Find and retrieve the legendary lost sword from the ancient cave."));
        AddQuest(new Quest("Deliver the Message", "Deliver the urgent message to the king in the northern castle."));
    }

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
        Debug.Log($"New quest added: {quest.title}");
    }

    public void UpdateQuest(string title, string updateInfo)
    {
        Quest quest = activeQuests.Find(q => q.title == title);
        if (quest != null)
        {
            quest.UpdateObjective(updateInfo);
        }
    }

    public void CompleteQuest(string title)
    {
        Quest quest = activeQuests.Find(q => q.title == title);
        if (quest != null)
        {
            quest.Complete();
            activeQuests.Remove(quest);
            Debug.Log($"Quest completed: {quest.title}");
        }
    }
}

[System.Serializable]
public class Quest
{
    public string title;
    public string description;
    public bool isComplete;

    public Quest(string title, string description)
    {
        this.title = title;
        this.description = description;
        isComplete = false;
    }

    public void UpdateObjective(string updateInfo)
    {
        Debug.Log($"Quest '{title}' updated: {updateInfo}");
    }

    public void Complete()
    {
        isComplete = true;
        Debug.Log($"Quest '{title}' completed.");
        PlayerManager.Instance.AddExperience(100);
    }
}
