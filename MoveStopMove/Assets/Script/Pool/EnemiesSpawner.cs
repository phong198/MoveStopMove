using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;
using System.Linq;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private GameUnit enemy;
    [SerializeField] private Transform poolParent;
    private int enemyAmounts = 6;

    private Vector3 randomPosition;
    private Vector3 position;
    private int xPos;
    private int zPos;
    public Transform _player;
    private void Start()
    {
        PoolSystem.Preload(enemy, enemyAmounts, poolParent);

        for(int i = 0; i < enemyAmounts; i++)
        {
            SpawnEnemies();
        }    
    }

    private void Update()
    {

    }

    private void SpawnEnemies()
    {
        do
        {
            xPos = Random.Range(-50, 50);
            zPos = Random.Range(-50, 50);
            position = new Vector3(xPos, 0, zPos);

            NavMeshHit closestHit;
            NavMesh.SamplePosition(position, out closestHit, 250f, NavMesh.AllAreas);
            randomPosition = closestHit.position;

        } while (Vector3.Distance(randomPosition, _player.position) < 7f);

        PoolSystem.Spawn(enemy, randomPosition, Quaternion.identity);
    }

}
