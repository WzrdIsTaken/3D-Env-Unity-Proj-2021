using UnityEngine;

// Specific movement for the cameras
public class CameraMovement : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] CameraMode cameraMode;
#pragma warning restore 649

    enum CameraMode { NONE, FOLLOW, ROTATE };
    Transform player;

    readonly Vector3 cameraFollowOffset = new Vector3(0, 0, -2);

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    void LateUpdate()
    {
        switch (cameraMode)
        {
            case CameraMode.FOLLOW:
                Follow();
                break;
            case CameraMode.ROTATE:
                Rotate();
                break;
            default:
                break;
        }
    }

    void Follow()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z) + cameraFollowOffset;
    }

    void Rotate()
    {
        transform.LookAt(player);
    }
}
