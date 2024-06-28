using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Start, PlayerTurn, EnemyTurn, Won, Lost }
    public GameState state;

    [SerializeField] private List<UnitManager> playerUnits;
    [SerializeField] private List<UnitManager> enemyUnits;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        state = GameState.Start;
        InitializeGame();
    }

    void InitializeGame()
    {
        TransitionToState(GameState.PlayerTurn);
    }

    void Update()
    {
        if (state == GameState.PlayerTurn)
            PlayerTurn();

        else if (state == GameState.EnemyTurn)
            StartCoroutine(EnemyTurn());
    }

    void PlayerTurn()
    {

    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2);
        TransitionToState(GameState.PlayerTurn);
    }

    void CheckWinCondition()
    {

    }

    void TransitionToState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.PlayerTurn:
                break;
            case GameState.EnemyTurn:
                break;
            case GameState.Won:
                break;
            case GameState.Lost:
                break;
        }
    }

    public void EndTurn()
    {
        if (state == GameState.PlayerTurn)
            TransitionToState(GameState.EnemyTurn);
        else if (state == GameState.EnemyTurn)
            TransitionToState(GameState.PlayerTurn);
    }
}

internal class UnitManager
{
}