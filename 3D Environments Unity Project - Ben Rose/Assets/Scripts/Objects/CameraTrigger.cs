using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] CameraManager.CameraLocations switchLocation;
    [SerializeField] GameObject pair;
#pragma warning restore 649

    CameraManager cameraManager;

    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();

        gameObject.SetActive(GetCameraLocation() > pair.GetComponent<CameraTrigger>().GetCameraLocation()); // Determine which trigger will be active first (lower enum value = closer to start)
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) 
        {
            cameraManager.PlayerCollidedWithTrigger(switchLocation);

            pair.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public int GetCameraLocation()
    {
        return (int)System.Convert.ChangeType(switchLocation, switchLocation.GetTypeCode());
    }
}

/* Other version, doesn't account for the player being on the trigger then going back the other way 
 
#pragma warning disable 649
    [SerializeField] CameraManager.CameraLocations cameFromLocation, goToLocation; // Came from / go to are the locations that the player visits when they progress one way through the level
#pragma warning restore 649

    CameraManager.CameraLocations currentCameraLocation;
    CameraManager cameraManager;

    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        currentCameraLocation = goToLocation;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) 
        {
            cameraManager.PlayerCollidedWithTrigger(currentCameraLocation);
            currentCameraLocation = currentCameraLocation == cameFromLocation ? goToLocation : cameFromLocation;
        }
    }
 */