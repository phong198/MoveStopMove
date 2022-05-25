using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private TMP_Text enemyName;
    [SerializeField]
    private Enemy enemy;

    // Update is called once per frame
    void Update()
    {
        enemyName.SetText(enemy.characterName.ToString());
        levelText.SetText("Lv: " + enemy.level.ToString());
        healthText.SetText(enemy.currentHealth.ToString() + "/" + enemy.maxHealth.ToString());
    }
}
