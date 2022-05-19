using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager>
{
    public enum GameState { gameUI, gameStart, gameOver, gameWin}
    public GameState gameState;

    public int enemyCount = 0;
    public int totalEnemiesPerStage;

    public int smallXpCount = 0;
    public int bigXpCount = 0;
    private void Awake()
    {
        gameState = GameState.gameUI;
        totalEnemiesPerStage = 20;
    }
    private void Update()
    {
        Debug.Log(smallXpCount);
        Debug.Log(bigXpCount);
    }
}
