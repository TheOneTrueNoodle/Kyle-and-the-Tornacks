using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.FindGameplayGrid);
    }

    public void ChangeState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.FindGameplayGrid:
                GridManager.Instance.FindGrid();
                break;
            case GameState.SelectUnits:
                BattleSetupManager.Instance.SelectHeroes();
                break;
            case GameState.SelectStartPositions:
                BattleSetupManager.Instance.SelectStartingPositions();
                break;
            case GameState.CombatLoop:
                BattleManager.Instance.CreateInitiativeOrder();
                break;
            case GameState.Victory:
                break;
            case GameState.Defeat:
                break;
        }

    }

    public enum GameState
    {
        FindGameplayGrid,
        SelectUnits,
        SelectStartPositions,
        CombatLoop,
        Victory,
        Defeat
    }
}