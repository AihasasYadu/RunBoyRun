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
        EventsManager.ResetGame += DestroyGameObject;
        currObjectType = PoolObjects.Platform;
        platformQ = new Queue<GameObject>();
        InitialPlatforms();
    }

    private void InitialPlatforms()
    {
        for (int i = 0; i < 5; i++)
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
        GameObject temp = PoolingController.Instance.SpawnFromPool(currObjectType, nextSpawnPoint);
        temp.GetComponent<PlatformController>().AddItemsOnPlatform();
        platformQ.Enqueue(temp);
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
