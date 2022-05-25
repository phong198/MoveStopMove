using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Map Object")]
public class MapObjects : ScriptableObject
{
    // Start is called before the first frame update
    public GameObject prefab;
    [Header("prefabPos")]
    public float posX;
    public float posY;
    public float posZ;
    [Header("prefabRot")]
    public float rotX;
    public float rotY;
    public float rotZ;
}
