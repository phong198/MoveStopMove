using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class IngameUIManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private TMP_Text aliveText;

    // Update is called once per frame
    void Update()
    {
        playerName.SetText(player.characterName);
        levelText.SetText("Level: " + player.level.ToString());
        expText.SetText("Exp: " + player.exp.ToString() + "/" + player.expToNextLevel.ToString());
        healthText.SetText(player.currentHealth.ToString() + "/" + player.maxHealth.ToString());
        aliveText.SetText("Alive: " + (GameFlowManager.Instance.enemiesLeftCount + 1).ToString() + "/" + (GameFlowManager.Instance.totalEnemiesPerStage + 1).ToString());

        if(GameFlowManager.Instance.gameState == GameFlowManager.GameState.gameOver)
        {
            gameObject.SetActive(false);
        }
    }
}
