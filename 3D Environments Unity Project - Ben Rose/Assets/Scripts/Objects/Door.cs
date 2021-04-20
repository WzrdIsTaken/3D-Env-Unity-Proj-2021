using UnityEngine;

// The door that you have to open
public class Door : InteractableObject
{
    public override void Interact(PlayerController player)
    {
        FindObjectOfType<DungeonManager>().OpenDoor();

        GetComponent<BoxCollider>().enabled = false;
        isInteractable = false;
    }
}