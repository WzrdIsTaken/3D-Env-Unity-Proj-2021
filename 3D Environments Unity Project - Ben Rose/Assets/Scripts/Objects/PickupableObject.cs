using UnityEngine;

// Base class for all objects that can be picked up
public class PickupableObject : InteractableObject
{
    protected Rigidbody rb;

    const float RAYCAST_COLLIDER_SIZE = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Create a collider around the object so its easier to raycast to
        GameObject raycastColliderGameObject = new GameObject("RaycastCollider");
        raycastColliderGameObject.transform.SetParent(gameObject.transform, false);
        raycastColliderGameObject.tag = "InteractableObject";
        raycastColliderGameObject.layer = LayerMask.NameToLayer("InteractableObjectRaycastCollider");
        BoxCollider raycastCollider = raycastColliderGameObject.AddComponent<BoxCollider>();
        raycastCollider.size = new Vector3(RAYCAST_COLLIDER_SIZE, RAYCAST_COLLIDER_SIZE, RAYCAST_COLLIDER_SIZE);
    }

    public override void Interact(PlayerController player)
    {
        player.SetCurrentlyHoldingObject(this);

        rb.constraints = RigidbodyConstraints.FreezeAll;
        gameObject.transform.parent = player.transform;
        gameObject.transform.position = player.GetPickupPoint().position;
    }

    public override void UnInteract(PlayerController player)
    {
        player.SetCurrentlyHoldingObject(null);

        gameObject.transform.parent = null;
        rb.constraints = RigidbodyConstraints.None;
    }
}