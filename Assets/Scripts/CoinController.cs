using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private const int PLAYER_LAYER = 8;

    private void Update()
    {
        StartCoroutine(NotInCameraView());
    }

    private IEnumerator NotInCameraView()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        if (transform.position.z < cameraPos.z)
        {
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer.Equals(PLAYER_LAYER))
        {
            EventsManager.Instance.UpdateScore();
            gameObject.SetActive(false);
        }
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
