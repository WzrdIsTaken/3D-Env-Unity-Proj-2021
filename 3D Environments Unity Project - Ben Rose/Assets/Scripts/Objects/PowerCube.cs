// A powercube that you pick up and insert into the portal
public class PowerCube : PickupableObject
{
    public override void Action(PlayerController player)
    {
        InteractableObject currentInteractingObject = player.GetCurrentlyInteractingObject();

        if (currentInteractingObject is Portal) currentInteractingObject.GetComponent<Portal>().StartPortal();
    }
}