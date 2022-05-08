using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;
    public bool playButtonClicked = false;

    private void Awake()
    {
        Instance = this;
    }

    private void ClickPlayButton()
    {
        playButtonClicked = true;
    }
}
