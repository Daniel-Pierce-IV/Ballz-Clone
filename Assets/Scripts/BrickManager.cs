using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField] private BrickPool brickPool;
    [SerializeField] private Transform[] spawnPoints;
    //[SerializeField] private int lowerBrickSpawnLimitBuffer = 2;
    //[SerializeField] private int upperBrickSpawnLimitBuffer = 1;

    private int currentPhase = 1;
    
    private void InitializeBrick(Brick brick, Vector3 position)
    {
        brick.transform.position = position;
        brick.SetHitpoints(currentPhase);
        brick.gameObject.SetActive(true);
    }

    public void SpawnBricks()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            InitializeBrick(brickPool.TakeObject(), spawnPoint.position);
        }

        currentPhase++;
        MoveBricks();
    }

    private void MoveBricks()
    {
        foreach (Brick brick in brickPool.GetObjectList())
        {
            if(brick.isActiveAndEnabled) brick.MoveDown();
        }
    }
}
