using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ranking;
    [SerializeField] private TMP_Text gold;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ranking.SetText((GameFlowManager.Instance.enemiesLeftCount + 1).ToString() + "/" + (GameFlowManager.Instance.totalEnemiesPerStage + 1).ToString());
        gold.SetText("Get: " + GameFlowManager.Instance.goldPerStage.ToString());
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
