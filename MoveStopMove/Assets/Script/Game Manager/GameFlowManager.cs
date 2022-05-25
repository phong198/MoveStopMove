using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager>
{
    public enum GameState { gameUI, gameStart, gameOver, gameWin }
    public GameState gameState;

    public int goldPerStage;
    public int totalPlayerGold;

    public int enemiesActiveInPool = 0;
    public int totalEnemiesPerStage;
    public int enemiesLeftCount;

    public int smallXpCount = 0;
    public int bigXpCount = 0;

    private void Awake()
    {
        totalPlayerGold = PlayerPrefs.GetInt("totalPlayerGold", 0);
        goldPerStage = 0;
        gameState = GameState.gameUI;
        totalEnemiesPerStage = 19;
        enemiesLeftCount = totalEnemiesPerStage;
    }
      

    public void IncreaseGoldWhenKill()
    {
        goldPerStage += Constant.GOLD_PER_KILL;
    }    

    public void GetGoldAfterStage()
    {
        goldPerStage += (totalEnemiesPerStage - enemiesLeftCount + 1) * Constant.GOLD_PER_RANK;
        totalPlayerGold += goldPerStage;
        SaveGold();
    }    

    public void SaveGold()
    {
        PlayerPrefs.SetInt("totalPlayerGold", totalPlayerGold);
        PlayerPrefs.Save();
    }    

    public void CheckGameStateWin()
    {
        if (enemiesLeftCount == 0)
        {
            gameState = GameState.gameWin;
        }
    }
}
