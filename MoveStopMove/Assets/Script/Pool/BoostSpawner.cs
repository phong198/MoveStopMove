using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoostSpawner : MonoBehaviour
{
    [SerializeField] private GameUnit boostHealth;
    [SerializeField] private GameUnit boostDamage;
    [SerializeField] private GameUnit boostSpeed;
    [SerializeField] private Transform poolParent;
    private Vector3 randomPosition;
    private Vector3 position;
    private int xPos;
    private int zPos;

    private float spawnBoostTime = 15f;
    private float spawnBoostTimer;
    void Start()
    {
        PoolSystem.Preload(boostHealth, 10, poolParent);
        PoolSystem.Preload(boostDamage, 10, poolParent);
        PoolSystem.Preload(boostSpeed, 10, poolParent);
        spawnBoostTimer = spawnBoostTime;
    }

    void Update()
    {
        if(GameFlowManager.Instance.gameState == GameFlowManager.GameState.gameStart)
        {
            spawnBoostTimer -= Time.deltaTime;
            if(spawnBoostTimer <= 0)
            {
                SpawnBoost(1);
                SpawnBoost(2);
                SpawnBoost(3);
                spawnBoostTimer = spawnBoostTime;
            }
        }
    }

    private void SpawnBoost(int boostID)
    {
        xPos = Random.Range(-50, 50);
        zPos = Random.Range(-50, 50);
        position = new Vector3(xPos, 0, zPos);

        NavMeshHit closestHit;
        NavMesh.SamplePosition(position, out closestHit, 250f, NavMesh.AllAreas);
        randomPosition = closestHit.position;
        switch(boostID)
        {
            case 1:
                PoolSystem.Spawn(boostHealth, randomPosition, Quaternion.identity);
                break;
            case 2:
                PoolSystem.Spawn(boostDamage, randomPosition, Quaternion.identity);
                break;
            case 3:
                PoolSystem.Spawn(boostSpeed, randomPosition, Quaternion.identity);
                break;
        }
    }    
}
