using UnityEngine;
using TMPro;

// Where you insert the power cube
public class PortalActivator : InteractableObject
{
#pragma warning disable 649
    [SerializeField] Portal portal;
    [SerializeField] Vector3 powerCubePos;
#pragma warning restore 649

    public override void DisplayMessage(bool isInRange, PlayerController player)
    {
        TMP_Text interactionText = player.GetInteractionText();

        interactionText.text = player.GetCurrentlyHoldingObject() is PowerCube ? "Press " + player.GetKey("Action") + " to insert the powercube" : "Insert the powercube to activate the portal";
        FadeInteractionText(isInRange, interactionText);
    }

    public void StartPortal()
    {
        portal.ActivatePortal();
        isInteractable = false;
    }

    public (Vector3, Quaternion) GetPowerCubeFramePosAndRot()
    {
        return (powerCubePos, transform.rotation);
    }
}
