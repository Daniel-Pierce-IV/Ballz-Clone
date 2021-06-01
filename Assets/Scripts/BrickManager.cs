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

    private List<BrickController> bricks = new List<BrickController>();
    private int currentPhase = 1;
    
    private void CreateBrick(Vector3 position)
    {
        BrickController brick = Instantiate(
            brickPrefab,
            position,
            Quaternion.identity,
            brickContainer.transform).
            GetComponent<BrickController>();
        
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
        foreach (BrickController brick in bricks)
        {
            brick.MoveDown();
        }
    }
}
