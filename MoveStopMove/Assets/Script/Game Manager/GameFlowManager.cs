using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager>
{
    public enum GameState { gameUI, gameStart, gameOver, gameWin}
    public GameState gameState;

    private void Awake()
    {
        gameState = GameState.gameUI;
    }
}
