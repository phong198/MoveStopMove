using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrowdSpawner : MonoBehaviour
{
    public GameObject npc;
    private GameObject newpeople;
    public GameObject crowdspawner;
    private int xPos;
    private int zPos;

    void Start()
    {
        for (int i = 0; i < 24; i++)
        {
            SpawnNPC();
        }
    }

    void SpawnNPC()
    {
            xPos = Random.Range(-50, 50);
            zPos = Random.Range(-50, 50);
            newpeople = Instantiate(npc, new Vector3(xPos, 0, zPos), Quaternion.identity);
            newpeople.transform.parent = transform;
    }
}
