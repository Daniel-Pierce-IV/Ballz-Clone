using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private BrickPool brickPool;
    [SerializeField] private PowerupPool powerupPool;
    [SerializeField] private int lowerSpawnLimitBuffer = 3;
    [SerializeField] private int upperSpawnLimitBuffer = 1;
    [SerializeField] private float brickDoubleHitpointsChance = 0.25f;

    private int currentPhase = 0;
    private List<Transform> selectedSpawnPoints = new List<Transform>();
    
    public void UpdateLevel()
    {
        currentPhase++;
        ChooseSpawnPoints();
        SpawnEntities();
        MoveEntities();
    }

    private void InitializeBrick(Brick brick, Vector3 position)
    {
        brick.transform.position = position;
        
        // Randomly double brick hitpoints
        if(UnityEngine.Random.Range(0f, 1f) < brickDoubleHitpointsChance)
            brick.SetHitpoints(currentPhase * 2);
        else brick.SetHitpoints(currentPhase);

        brick.gameObject.SetActive(true);
    }

    private void SpawnBricks()
    {
        foreach (var spawnPoint in selectedSpawnPoints)
        {
            InitializeBrick(brickPool.TakeObject(), spawnPoint.position);
        }
    }

    private void InitializePowerup(Powerup powerup, Vector3 position)
    {
        powerup.transform.position = position;
        powerup.gameObject.SetActive(true);
    }

    private void SpawnPowerup()
    {
        int index = UnityEngine.Random.Range(0, selectedSpawnPoints.Count);
        Vector3 powerupPosition = selectedSpawnPoints[index].position;
        selectedSpawnPoints.RemoveAt(index);
        InitializePowerup(powerupPool.TakeObject(), powerupPosition);
    }

    private void SpawnEntities()
    {
        SpawnPowerup();
        SpawnBricks();
    }

    private void MoveEntities()
    {
        foreach (Brick brick in brickPool.GetObjectList())
        {
            if(brick.isActiveAndEnabled) brick.Move();
        }

        foreach (Powerup powerup in powerupPool.GetObjectList())
        {
            if (powerup.isActiveAndEnabled) powerup.Move();
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
