using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    // Start is called before the first frame update

    [System.Serializable]
    public class Pool
    {
        public string poolTag;
        public GameObject _enemy;
        public int poolSize;
    }

    #region singleton
    public static EnemyPool PoolAccess;
    private void Awake()
    {
        PoolAccess = this;
    }
    #endregion


    public List<Pool> enemyList;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in enemyList)
        {
            Queue<GameObject> enemyPool = new Queue<GameObject>();
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject enemyObj = Instantiate(pool._enemy);
                enemyObj.transform.parent = pool._enemy.transform.parent;
                enemyObj.SetActive(false);
                enemyPool.Enqueue(enemyObj);
            }
            poolDictionary.Add(pool.poolTag, enemyPool);
        }
    }

    public void SpawnFromPool (string poolTag, Vector3 position, Quaternion rotation)
    {
        GameObject enemyToSpawn = poolDictionary[poolTag].Dequeue();
        enemyToSpawn.SetActive(true);
        enemyToSpawn.transform.position = position;
        enemyToSpawn.transform.rotation = rotation;

        poolDictionary[poolTag].Enqueue(enemyToSpawn);
    }    
}
