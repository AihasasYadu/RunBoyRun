using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Transform nextSpawnPoint;
    [SerializeField] private Transform[] lanePosArr = new Transform[3];
    private const int PLAYER_LAYER = 8;
    private BoxCollider boxCollider;
    public Transform GetNextSpawnPoint { get { return nextSpawnPoint; } }

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
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
