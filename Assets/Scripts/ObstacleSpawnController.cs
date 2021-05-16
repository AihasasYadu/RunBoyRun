using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnController : MonoBehaviour
{
    private Transform spawnLocation;
    private PoolObjects currObjectType;

    private void Awake()
    {
        spawnLocation = GetComponent<Transform>();
    }

    private void RandomizeCurrentObstacle()
    {
        currObjectType = (PoolObjects)Random.Range((int)PoolObjects.JumpObstacles, (int)PoolObjects.Last);
    }

    public GameObject Spawn(Transform lineStart, Transform lineEnd)
    {
        RandomizeCurrentObstacle();
        float zRange = lineEnd.position.z - lineStart.position.z;

        Vector3 spawnPosition = new Vector3(lineStart.position.x, lineStart.position.y,
                                            lineStart.position.z + (zRange * UnityEngine.Random.value));

        spawnLocation.position = spawnPosition;

        GameObject spawnedObstacle = PoolingController.Instance.SpawnFromPool(currObjectType, spawnLocation);
        return spawnedObstacle;
    }
}
