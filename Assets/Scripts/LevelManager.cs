using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private BrickPool brickPool;
    [SerializeField] private int lowerSpawnLimitBuffer = 3;
    [SerializeField] private int upperSpawnLimitBuffer = 1;

    private int currentPhase = 0;
    private List<Transform> selectedSpawnPoints = new List<Transform>();
    
    public void UpdateLevel()
    {
        currentPhase++;
        ChooseSpawnPoints();
        SpawnBricks();
        MoveEntities();
    }

    private void InitializeBrick(Brick brick, Vector3 position)
    {
        brick.transform.position = position;
        brick.SetHitpoints(currentPhase);
        brick.gameObject.SetActive(true);
    }

    private void SpawnBricks()
    {
        foreach (var spawnPoint in selectedSpawnPoints)
        {
            InitializeBrick(brickPool.TakeObject(), spawnPoint.position);
        }
    }

    private void MoveEntities()
    {
        foreach (Brick brick in brickPool.GetObjectList())
        {
            // TODO replace with more efficient code
            if(brick.isActiveAndEnabled) brick.GetComponent<InterpolatedMover>().MoveDown();
        }
    }

    private List<Transform> ChooseSpawnPoints()
    {
        selectedSpawnPoints.Clear();
        selectedSpawnPoints.AddRange(spawnPoints);

        // Selects a number between lowerSpawnLimitBuffer and the total
        // number of spawn points minus upperSpawnLimitBuffer (both inclusive)
        int numBricksToSpawn = UnityEngine.Random.Range(
            lowerSpawnLimitBuffer,
            selectedSpawnPoints.Count - (upperSpawnLimitBuffer - 1));

        int numBricksToEliminate = selectedSpawnPoints.Count - numBricksToSpawn;

        for (int i = 0; i < numBricksToEliminate; i++)
        {
            selectedSpawnPoints.RemoveAt(
                UnityEngine.Random.Range(0, selectedSpawnPoints.Count));
        }

        return selectedSpawnPoints;
    }
}
