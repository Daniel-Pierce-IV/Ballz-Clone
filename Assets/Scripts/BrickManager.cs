using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField] private BrickPool brickPool;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int lowerBrickSpawnLimitBuffer = 2;
    [SerializeField] private int upperBrickSpawnLimitBuffer = 1;

    private int currentPhase = 1;
    private List<Transform> selectedSpawnPoints = new List<Transform>();
    
    private void InitializeBrick(Brick brick, Vector3 position)
    {
        brick.transform.position = position;
        brick.SetHitpoints(currentPhase);
        brick.gameObject.SetActive(true);
    }

    public void SpawnBricks()
    {
        foreach (var spawnPoint in ChooseBrickSpawnPoints())
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

    private List<Transform> ChooseBrickSpawnPoints()
    {
        selectedSpawnPoints.Clear();
        selectedSpawnPoints.AddRange(spawnPoints);

        // Selects a number between lowerBrickSpawnLimitBuffer and the total
        // number of spawn points minus upperBrickSpawnLimitBuffer (both inclusive)
        int numBricksToSpawn = UnityEngine.Random.Range(
            lowerBrickSpawnLimitBuffer,
            selectedSpawnPoints.Count - (upperBrickSpawnLimitBuffer - 1));

        int numBricksToEliminate = selectedSpawnPoints.Count - numBricksToSpawn;

        for (int i = 0; i < numBricksToEliminate; i++)
        {
            selectedSpawnPoints.RemoveAt(
                UnityEngine.Random.Range(0, selectedSpawnPoints.Count));
        }

        return selectedSpawnPoints;
    }
}
