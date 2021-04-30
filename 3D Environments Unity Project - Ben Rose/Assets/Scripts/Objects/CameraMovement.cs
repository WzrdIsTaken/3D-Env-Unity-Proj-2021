﻿using UnityEngine;

// Specific movement for the cameras
public class CameraMovement : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] CameraMode cameraMode;
#pragma warning restore 649

    [HideInInspector] public enum CameraMode { NONE, FOLLOW, ROTATE, ADVANCED_ROTATE };
    Transform player;

    // CameraMode specific stuff: 

    /**
     * Currently, these values are just hardcoded in because I only have one type of each camera in the scene and having them all be serialized
     * when each type of camera only uses a few (or none) of them would be pretty ugly. I started work on an editor script but its very low
     * priority (see Assets/Scripts/Editor).
    **/

    // Advanced rotate variables
    Vector3 CAMERA_START_POSITION;
    readonly Vector3 CAMERA_FOLLOW_OFFSET = new Vector3(0, 0, -2);

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        CAMERA_START_POSITION = transform.position;
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
            case CameraMode.ADVANCED_ROTATE:
                AdvancedRotate();
                break;
            default:
                break;
        }
    }

    void Follow()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z) + CAMERA_FOLLOW_OFFSET;
    }

    void Rotate()
    {
        transform.LookAt(player);
    }

    void AdvancedRotate()
    {
        transform.LookAt(player);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float yOffset = Mathf.Clamp(distanceToPlayer / 3, 0, 4);
        transform.position = new Vector3(transform.position.x, CAMERA_START_POSITION.y - yOffset, transform.position.z);
    }

    public CameraMode GetCameraMode()
    {
        return cameraMode;
    }
}