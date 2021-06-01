using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject brickContainer;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int lowerBrickSpawnLimitBuffer = 2;
    [SerializeField] private int upperBrickSpawnLimitBuffer = 1;

    private List<Brick> bricks = new List<Brick>();
    private int currentPhase = 1;
    
    private void CreateBrick(Vector3 position)
    {
        Brick brick = Instantiate(
            brickPrefab,
            position,
            Quaternion.identity,
            brickContainer.transform).
            GetComponent<Brick>();
        
        brick.SetHitpoints(currentPhase);
        bricks.Add(brick);
    }

    public void CreateBricks()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            CreateBrick(spawnPoint.position);
        }

        currentPhase++;
        MoveBricks();
    }

    private void MoveBricks()
    {
        foreach (Brick brick in bricks)
        {
            brick.MoveDown();
        }
    }
}
