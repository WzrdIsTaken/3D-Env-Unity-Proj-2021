using UnityEngine;

// A powercube that you pick up and insert into the portal
public class PowerCube : PickupableObject
{
    public override void Action(PlayerController player)
    {
        InteractableObject currentInteractingObject = player.GetCurrentlyInteractingObject();

        if (currentInteractingObject is PortalActivator) 
        {
            base.UnInteract(player);

            PortalActivator portalActivator = currentInteractingObject.GetComponent<PortalActivator>();
            portalActivator.StartPortal();
            (Vector3, Quaternion) posAndRot = portalActivator.GetPowerCubeFramePosAndRot();
            transform.position = posAndRot.Item1;
            transform.rotation = posAndRot.Item2;

            GetComponent<BoxCollider>().enabled = false;
            rb.useGravity = false;
            isInteractable = false;
        }
    }
}