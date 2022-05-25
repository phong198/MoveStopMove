using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Map")]
public class MapInfo : ScriptableObject
{
    // Start is called before the first frame update
    public int mapID;
    public MapObjects[] mapObjects;
}
