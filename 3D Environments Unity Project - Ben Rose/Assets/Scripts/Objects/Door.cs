using UnityEngine;

// The door that you have to open
public class Door : InteractableObject
{
    public override void Interact(PlayerController player)
    {
        // GameObject.Find("DungeonManager").GetComponent<DungeonManager>().OpenDoor();

        SetInteractableState(false);
    }
}