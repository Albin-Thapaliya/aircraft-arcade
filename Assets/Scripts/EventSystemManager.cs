using System.Collections.Generic;
using UnityEngine;

public class EventSystemManager : MonoBehaviour
{
    public static EventSystemManager Instance { get; private set; }

    private List<GameEvent> events = new List<GameEvent>();

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
        RegisterEvent(new WeatherChangeEvent("Sudden Storm", WeatherType.Storm, 0.1f));
        RegisterEvent(new EncounterEvent("Ambush", EncounterType.EnemyAmbush, 0.05f));
        RegisterEvent(new EncounterEvent("Mysterious Merchant", EncounterType.FriendlyEncounter, 0.02f));
    }

    public void RegisterEvent(GameEvent gameEvent)
    {
        events.Add(gameEvent);
    }

    void Update()
    {
        foreach (var gameEvent in events)
        {
            gameEvent.TryActivate();
        }
    }

    public void TriggerEvent(string eventName)
    {
        foreach (var gameEvent in events)
        {
            if (gameEvent.name == eventName)
            {
                gameEvent.Activate();
                break;
            }
        }
    }
}

public abstract class GameEvent
{
    public string name;
    public bool isActive;
    public float activationChance;

    protected GameEvent(string name, float activationChance)
    {
        this.name = name;
        this.activationChance = activationChance;
    }

    public virtual void TryActivate()
    {
        if (Random.value < activationChance)
        {
            Activate();
        }
    }

    public abstract void Activate();
    public abstract void ProcessEvent();
}

public class WeatherChangeEvent : GameEvent
{
    private WeatherType type;

    public WeatherChangeEvent(string name, WeatherType type, float activationChance)
        : base(name, activationChance)
    {
        this.type = type;
    }

    public override void Activate()
    {
        isActive = true;
        Debug.Log($"Weather change activated: {type}");
        switch (type)
        {
            case WeatherType.Rain:
                WeatherEffects.SetRainy();
                break;
            case WeatherType.Storm:
                WeatherEffects.SetStormy();
                break;
            case WeatherType.Snow:
                WeatherEffects.SetSnowy();
                break;
            case WeatherType.Clear:
                WeatherEffects.SetClear();
                break;
        }
    }

    public override void ProcessEvent()
    {
        WeatherEffects.ContinueWeatherEffect(type);
    }
}

public class EncounterEvent : GameEvent
{
    private EncounterType type;

    public EncounterEvent(string name, EncounterType type, float activationChance)
        : base(name, activationChance)
    {
        this.type = type;
    }

    public override void Activate()
    {
        isActive = true;
        Debug.Log($"Encounter event activated: {type}");
        switch (type)
        {
            case EncounterType.EnemyAmbush:
                EnemyManager.SpawnEnemies("Ambush", 5);
                break;
            case EncounterType.FriendlyEncounter:
                NPCManager.SpawnFriendlyNPC("Mysterious Merchant");
                break;
            case EncounterType.NeutralEncounter:
                ScenarioManager.TriggerScenario("Ancient Ruins Discovery");
                break;
        }
    }

    public override void ProcessEvent()
    {
        EncounterManager.UpdateEncounter(type);
    }
}


public class EncounterEvent : GameEvent
{
    private EncounterType type;

    public EncounterEvent(string name, EncounterType type, float activationChance)
        : base(name, activationChance)
    {
        this.type = type;
    }

    public override void Activate()
    {
        isActive = true;
        Debug.Log($"Encounter event activated: {type}");
    }

    public override void ProcessEvent()
    {
        EncounterManager.UpdateEncounter(type);
    }
}

public enum WeatherType
{
    Clear,
    Rain,
    Storm,
    Snow
}

public enum EncounterType
{
    NeutralEncounter,
    EnemyAmbush,
    FriendlyEncounter
}
