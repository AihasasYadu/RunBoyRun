using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Transform nextSpawnPoint;
    [SerializeField] private Transform[] lanePosArr = new Transform[3];
    [SerializeField] private ObstacleSpawnController obstacleSpawner;
    [SerializeField] private CoinsSpawnController coinsSpawner;
    [SerializeField] private List<Transform> spawnLines = new List<Transform>(6);
    private const int PLAYER_LAYER = 8;
    private BoxCollider boxCollider;
    public Transform GetNextSpawnPoint { get { return nextSpawnPoint; } }

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void AddItemsOnPlatform()
    {
        CreateObstacleCourse();
        SpawnCoinsOnPlatform();
    }

    private void CreateObstacleCourse()
    {
        for (int i = 0; i < spawnLines.Count; i+=2)
        {
            obstacleSpawner.Spawn(spawnLines[i], spawnLines[i+1]);
        }
    }

    private void SpawnCoinsOnPlatform()
    {
        int size = Random.Range(0, spawnLines.Count);
        for (int i = 0; i < size; i+=2)
        {
            coinsSpawner.Spawn(spawnLines[i], spawnLines[i + 1]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer.Equals(PLAYER_LAYER))
        {
            PlatformManager.Instance.AddToPlatformQueue();
            boxCollider.enabled = false;
            StartCoroutine(DisableGameObject());
        }
    }

    private IEnumerator DisableGameObject()
    {
        yield return new WaitForSeconds(1f);
        boxCollider.enabled = true;
        gameObject.SetActive(false);
    }
}
