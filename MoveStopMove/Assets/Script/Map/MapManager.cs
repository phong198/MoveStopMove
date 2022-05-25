using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public MapInfo[] map;
    public Transform parent;
    public int mapID;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        mapID = PlayerPrefs.GetInt("mapID", 0);

        if (mapID > (map.Length - 1))
        {
            mapID = map.Length - 1;
        }
        LoadMap(map[mapID]);
    }

    public void ToNextMap()
    {
        mapID += 1;
        PlayerPrefs.SetInt("mapID", mapID);
        PlayerPrefs.Save();
    }

    private void LoadMap(MapInfo map)
    {
        foreach (MapObjects mapObject in map.mapObjects)
        {
            mapObject.prefab.transform.position = new Vector3(mapObject.posX, mapObject.posY, mapObject.posZ);
            mapObject.prefab.transform.rotation = Quaternion.Euler(mapObject.rotX, mapObject.rotY, mapObject.rotZ);
            Instantiate(mapObject.prefab, parent);
        }
    }
}
