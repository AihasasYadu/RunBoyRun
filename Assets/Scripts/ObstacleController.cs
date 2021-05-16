using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private const int PLAYER_LAYER = 8;
    private bool playerCollided;

    private void OnEnable()
    {
        playerCollided = false;
    }

    private void Update()
    {
        StartCoroutine(NotInCameraView());
    }

    private IEnumerator NotInCameraView()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        if(transform.position.z < cameraPos.z)
        {
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer.Equals(PLAYER_LAYER) && !playerCollided)
        {
            EventsManager.Instance.ObstacleCollision();
            playerCollided = true;
        }
    }
}
