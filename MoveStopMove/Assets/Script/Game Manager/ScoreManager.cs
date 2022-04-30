using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private Character character;

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText(character.score.ToString());
    }
}
