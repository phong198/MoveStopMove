using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExpSpawner : MonoBehaviour
{
    [SerializeField] private GameUnit expSmall;
    [SerializeField] private GameUnit expBig;
    [SerializeField] private Transform expPoolParent;

    private int expSmallAmount = 20;
    private int expBigAmount = 5;

    private Vector3 randomPosition;
    private Vector3 position;
    private int xPos;
    private int zPos;
    void Start()
    {
        PoolSystem.Preload(expSmall, expSmallAmount, expPoolParent);
        PoolSystem.Preload(expBig, expBigAmount, expPoolParent);
        SpawnExpAtStart(expSmallAmount, 1);
        SpawnExpAtStart(expBigAmount, 2);
    }

    private void Update()
    {
        CheckExpAmount(GameFlowManager.Instance.smallXpCount, expSmallAmount, 1);
        CheckExpAmount(GameFlowManager.Instance.bigXpCount, expBigAmount, 2);

    }

    private void SpawnExpAtStart(int expAmount, int expID)
    {
        for (int i = 0; i < expAmount; ++i)
        {
            SpawnExp(expID);
        }
    }

    private void CheckExpAmount(int expCount, int expAmount, int expID)
    {
        if(expCount < expAmount)
        {
            SpawnExp(expID);
        }
    }

    private void SpawnExp(int expID)
    {
        xPos = Random.Range(-50, 50);
        zPos = Random.Range(-50, 50);
        position = new Vector3(xPos, 0, zPos);

        NavMeshHit closestHit;
        NavMesh.SamplePosition(position, out closestHit, 250f, NavMesh.AllAreas);
        randomPosition = closestHit.position;
        switch (expID)
        {
            case 1:
                PoolSystem.Spawn(expSmall, randomPosition, Quaternion.identity);
                GameFlowManager.Instance.smallXpCount++;
                break;
            case 2:
                PoolSystem.Spawn(expBig, randomPosition, Quaternion.identity);
                GameFlowManager.Instance.bigXpCount++;
                break;
        }
    }
}
