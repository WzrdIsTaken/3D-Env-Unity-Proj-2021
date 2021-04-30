using UnityEngine;
using UnityEngine.Events;

public class CameraTrigger : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] CameraManager.CameraLocations switchLocation;
    [SerializeField] GameObject pair = null; // If a trigger only triggers a cutscene, then its ok for it to not have a pair
    [SerializeField] SpecialAction specialAction;
#pragma warning restore 649

    CameraManager cameraManager;

    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        
        gameObject.SetActive(GetCameraLocation() > (pair ? pair.GetComponent<CameraTrigger>().GetCameraLocation() : int.MinValue)); // Determine which trigger will be active first (lower enum value = closer to start)
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) 
        {
            cameraManager.PlayerCollidedWithTrigger(switchLocation);

            specialAction.TriggerAction();
            if (pair) pair.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public int GetCameraLocation()
    {
        return (int)System.Convert.ChangeType(switchLocation, switchLocation.GetTypeCode());
    }

    [System.Serializable]
    class SpecialAction
    {
        public UnityEvent specialAction;
        public bool actionCanOnlyBeTriggeredOnce;

        bool actionTriggered = false;

        public SpecialAction(UnityEvent _specialAction, bool _actionCanOnlyBeTriggeredOnce)
        {
            specialAction = _specialAction;
            actionCanOnlyBeTriggeredOnce = _actionCanOnlyBeTriggeredOnce;
        }

        public void TriggerAction()
        {
            if (actionCanOnlyBeTriggeredOnce)
            {
                if (actionTriggered) return;
                actionTriggered = true;
            }

            specialAction?.Invoke();
        }
    }
}