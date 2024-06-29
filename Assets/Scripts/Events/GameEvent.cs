using UnityEngine;
using System;

public class GameEvent
{
    public string eventName;
    public Action onTrigger;

    public GameEvent(string name, Action action)
    {
        eventName = name;
        onTrigger = action;
    }

    public void TriggerEvent()
    {
        onTrigger?.Invoke();
    }
}

public class EventManager : MonoBehaviour
{
    public GameEvent[] events;

    void Start()
    {
        events = new GameEvent[]
        {
            new GameEvent("Meteor Shower", () => Debug.Log("A meteor shower has begun!")),
            new GameEvent("Earthquake", () => Debug.Log("An earthquake is shaking the ground!")),
            new GameEvent("Alien Invasion", () => Debug.Log("Aliens are invading the planet!"))
        };
    }

    public void TriggerEventByName(string eventName)
    {
        foreach (var gameEvent in events)
        {
            if (gameEvent.eventName == eventName)
            {
                gameEvent.TriggerEvent();
                break;
            }
        }
    }
}