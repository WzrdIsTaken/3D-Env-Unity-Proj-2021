using UnityEngine;

// The door that you have to open
public class Door : InteractableObject
{
    public override void Interact(PlayerController player)
    {
        StartCoroutine(FindObjectOfType<DungeonManager>().StartAnimation(DungeonManager.Animation.OPEN_DOOR));

        GetComponent<BoxCollider>().enabled = false;
        isInteractable = false;
    }
}