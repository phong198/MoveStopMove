using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    // Start is called before the first frame update
    public UnityAction onCharacterDie;
    public void CharacterDie()
    {
        if (onCharacterDie != null)
        {
            onCharacterDie();
        }
    }
}
