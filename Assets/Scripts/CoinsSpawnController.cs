using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawnController : MonoBehaviour
{
    private Transform spawnLocation;
    private PoolObjects currObjectType;

    private void Awake()
    {
        spawnLocation = GetComponent<Transform>();
        currObjectType = PoolObjects.Coins;
    }

    public GameObject Spawn(Transform lineStart, Transform lineEnd)
    {
        float zRange = lineEnd.position.z - lineStart.position.z;

        Vector3 spawnPosition = new Vector3(lineStart.position.x, lineStart.position.y,
                                            lineStart.position.z + (zRange * UnityEngine.Random.value));

        spawnLocation.position = spawnPosition;

        GameObject spawnedObstacle = PoolingController.Instance.SpawnFromPool(currObjectType, spawnLocation);
        return spawnedObstacle;
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventsManager.ResetGame -= DestroyGameObject;
    }
}
