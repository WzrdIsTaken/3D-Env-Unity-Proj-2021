using UnityEngine;

// Base class for all objects that can be picked up
public class PickupableObject : InteractableObject
{
    protected Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Interact(PlayerController player)
    {
        player.SetCurrentlyHoldingObject(this);

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        gameObject.transform.parent = player.transform;
        gameObject.transform.position = player.GetPickupPoint().position;
    }

    public override void UnInteract(PlayerController player)
    {
        player.SetCurrentlyHoldingObject(null);

        gameObject.transform.parent = null;
        rb.useGravity = true;
    }
}