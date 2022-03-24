using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    private Vector3 position;
    private int xPos;
    private int zPos;

    private void Start()
    {
        SpawnEnemies();
        StartCoroutine(countinueSpawn());
    }
    IEnumerator countinueSpawn()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            EnemyPool.PoolAccess.CountActiveObject();

            if (EnemyPool.PoolAccess.activeObjCount < EnemyPool.PoolAccess.enemyList.Count) 
            {
                Debug.Log("aaa");
                SpawnEnemies();
            }
        }
    }
    private void SpawnEnemies()
    {
        xPos = Random.Range(-50, 50);
        zPos = Random.Range(-50, 50);
        position = new Vector3(xPos, 0, zPos);
        EnemyPool.PoolAccess.SpawnFromPool("Enemy", position, Quaternion.identity);
    }

}
