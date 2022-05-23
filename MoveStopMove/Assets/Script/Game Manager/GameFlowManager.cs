using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager>
{
    public enum GameState { gameUI, gameStart, gameOver, gameWin }
    public GameState gameState;

    public int enemiesActiveInPool = 0;
    public int totalEnemiesPerStage;
    public int enemiesLeftCount;

    public int smallXpCount = 0;
    public int bigXpCount = 0;
    private void Awake()
    {
        gameState = GameState.gameUI;
        totalEnemiesPerStage = 9;
        enemiesLeftCount = totalEnemiesPerStage;
    }

    private void Update()
    {
        Debug.Log("enemiesActiveInPool: " + enemiesActiveInPool);
    }

    public void CheckGameStateWin()
    {
        if (enemiesLeftCount == 0)
        {
            gameState = GameState.gameWin;
        }
    }
}
