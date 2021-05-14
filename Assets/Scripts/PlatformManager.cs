using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoSingletonGeneric<PlatformManager>
{
    [SerializeField] private Transform nextSpawnPoint;
    private PoolObjects currObjectType;
    private Queue<GameObject> platformQ;

    private void Start()
    {
        currObjectType = PoolObjects.Platform;
        platformQ = new Queue<GameObject>();
        InitialPlatforms();
    }

    private void InitialPlatforms()
    {
        platformQ.Enqueue(PoolingController.Instance.SpawnFromPool(currObjectType, nextSpawnPoint));
        for(int i = 0; i < 5; i++)
        {
            AddToPlatformQueue();
        }
    }

    public void AddToPlatformQueue()
    {
        foreach (GameObject item in platformQ)
        {
            nextSpawnPoint = item.GetComponent<PlatformController>().GetNextSpawnPoint;
        }
        platformQ.Enqueue(PoolingController.Instance.SpawnFromPool(currObjectType, nextSpawnPoint));
    }
}
