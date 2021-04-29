using UnityEngine;

// Changes between cameras
public class CameraManager : MonoBehaviour
{
    [HideInInspector] public enum CameraLocations { START_ROOM, CORRIDOR, MAIN_ROOM, CUBBIES };

#pragma warning disable 649
    [SerializeField] KeyCameraData[] cameras;
#pragma warning restore 649

    CameraLocations currentCameraLocation;

    PlayerController player;
    Camera currentCamera;

    void Awake() // Awake because we want the player camera to be set before the PlayerController starts
    {
        player = FindObjectOfType<PlayerController>();

        currentCamera = GetCameraWithKey(CameraLocations.START_ROOM).camera;
        player.SetNewCamera(currentCamera);
    }

    public void PlayerCollidedWithTrigger(CameraLocations location)
    {
        KeyCameraData keyCameraData = GetCameraWithKey(location);

        currentCamera.gameObject.SetActive(false);
        currentCamera = keyCameraData.camera;
        currentCamera.gameObject.SetActive(true);

        if (!keyCameraData.dontMakePlayerRotationPoint) 
        {
            if (keyCameraData.cameraMovement == CameraMovement.CameraMode.ROTATE || keyCameraData.cameraMovement == CameraMovement.CameraMode.ADVANCED_ROTATE) 
            {
                // Movement in the main room sometimes feels a bit off
                // I don't know if its placebo (xd) but setting the camera rotation back to 0 might help?
                // Todo: Experiement with this
            }
            player.SetNewCamera(currentCamera);
        }
    }

    KeyCameraData GetCameraWithKey(CameraLocations location) // Could precalculate all kvp's with a dict
    {
        foreach (KeyCameraData pair in cameras)
        {
            if (pair.location == location) 
            {
                if (currentCamera) currentCamera.tag = "Untagged";
                pair.camera.gameObject.tag = "MainCamera";
                return pair;
            }
        }

        throw new System.ArgumentException(location.ToString() + " does not have a KeyCameraPair match");
    }

    [System.Serializable]
    struct KeyCameraData
    {
        public CameraLocations location;
        public Camera camera;
        public bool dontMakePlayerRotationPoint;
        [HideInInspector] public CameraMovement.CameraMode cameraMovement;

        public KeyCameraData(CameraLocations _location, Camera _camera, bool _dontMakePlayerRotationPoint)
        {
            location = _location;
            camera = _camera;
            dontMakePlayerRotationPoint = _dontMakePlayerRotationPoint;

            cameraMovement = camera.GetComponent<CameraMovement>().GetCameraMode();
        }
    }
}
