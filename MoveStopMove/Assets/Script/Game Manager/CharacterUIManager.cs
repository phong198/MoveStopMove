using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private Character character;

    // Update is called once per frame
    void Update()
    {
        healthText.SetText(character.currentHealth.ToString() + "/" + character.maxHealth.ToString());
    }
}
