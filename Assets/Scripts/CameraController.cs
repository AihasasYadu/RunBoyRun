using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CharacterController player;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 targetPos = player.transform.position + offset;
        targetPos.x = player.transform.position.x;
        transform.position = targetPos;
    }
}