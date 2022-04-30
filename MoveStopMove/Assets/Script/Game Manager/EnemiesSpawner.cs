using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    private Vector3 position;
    private int xPos;
    private int zPos;
    public Transform _player;

    private void Start()
    {
        //SpawnEnemies();
        //StartCoroutine(countinueSpawn());
    }

    private void Update()
    {

        if (EnemyPool.PoolAccess.activeObjCount < EnemyPool.PoolAccess.enemyList[0].poolSize)
        {
            SpawnEnemies();
        }
        Debug.Log(EnemyPool.PoolAccess.activeObjCount);
    }
    //IEnumerator countinueSpawn()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForFixedUpdate();

    //        if (EnemyPool.PoolAccess.activeObjCount < EnemyPool.PoolAccess.enemyList[0].poolSize)
    //        {
    //            SpawnEnemies();
    //        }
    //    }
    //}
    private void SpawnEnemies()
    {
        do
        {
            xPos = Random.Range(-50, 50);
            zPos = Random.Range(-50, 50);
            position = new Vector3(xPos, 0, zPos);
        } while (Vector3.Distance(position, _player.position) < 7f);

        EnemyPool.PoolAccess.SpawnFromPool(Constant.POOL_ENEMY, position, Quaternion.identity);


    }

}
