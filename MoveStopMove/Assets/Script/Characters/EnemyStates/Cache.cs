using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<GameObject, Character> dic = new Dictionary<GameObject, Character>();


    public static Character GetCharacter(GameObject go)
    {
        if (go != null && !dic.ContainsKey(go))
        {
            Character character = go.GetComponent<Character>();

            if (character != null)
            {
                dic.Add(go, character);
            }

            return character;
        }

        return dic[go];
    }
}
