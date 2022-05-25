using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheat : MonoBehaviour
{
    // Start is called before the first frame update
    public void IncreaseGold()
    {
        GameFlowManager.Instance.totalPlayerGold += 1000;
        GameFlowManager.Instance.SaveGold();
    }

    public void ResetGold()
    {
        GameFlowManager.Instance.totalPlayerGold = 0;
        GameFlowManager.Instance.SaveGold();
    }

    public void ResetMap()
    {
        PlayerPrefs.SetInt("mapID", 0);
        PlayerPrefs.SetInt("totalEnemiesPerStage", 9);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
