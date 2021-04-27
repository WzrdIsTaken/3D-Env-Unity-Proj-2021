﻿using UnityEngine;

// Changes between cameras
public class CameraManager : MonoBehaviour
{
    [HideInInspector] public enum CameraLocations { START_ROOM, CORRIDOR, MAIN_ROOM_ONE, MAIN_ROOM_TWO };

#pragma warning disable 649
    [SerializeField] KeyCameraPair[] cameras;
#pragma warning restore 649

    CameraLocations currentCameraLocation;

    PlayerController player;
    Camera currentCamera;

    void Awake() // Awake because we want the player camera to be set before the PlayerController starts
    {
        player = FindObjectOfType<PlayerController>();

        currentCamera = GetCameraWithKey(CameraLocations.START_ROOM);
        player.SetNewCamera(currentCamera);
    }

    public void PlayerCollidedWithTrigger(CameraLocations location)
    {
        currentCamera.gameObject.SetActive(false);
        currentCamera = GetCameraWithKey(location);
        currentCamera.gameObject.SetActive(true);

        player.SetNewCamera(currentCamera);
    }

    Camera GetCameraWithKey(CameraLocations location) // Could precalculate all kvp's with a dict
    {
        foreach (KeyCameraPair pair in cameras)
        {
            if (pair.location == location) 
            {
                if (currentCamera) currentCamera.tag = "Untagged";
                pair.camera.gameObject.tag = "MainCamera";
                return pair.camera;
            }
        }

        throw new System.ArgumentException(location.ToString() + " does not have a KeyCameraPair match");
    }

    [System.Serializable]
    struct KeyCameraPair
    {
        public CameraLocations location;
        public Camera camera;

        public KeyCameraPair(CameraLocations _location, Camera _camera)
        {
            location = _location;
            camera = _camera;
        }
    }
}
