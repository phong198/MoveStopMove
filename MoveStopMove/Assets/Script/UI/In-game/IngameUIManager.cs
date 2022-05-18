using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class IngameUIManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text expText;

    // Update is called once per frame
    void Update()
    {
        levelText.SetText("Level: " + player.level.ToString());
        expText.SetText("Exp: " + player.exp.ToString() + "/" + player.expToNextLevel.ToString());
    }
}
