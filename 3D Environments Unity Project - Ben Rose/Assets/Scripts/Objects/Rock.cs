using UnityEngine;

// A rock that you can pick up and throw
public class Rock : PickupableObject
{
    public override void Action(PlayerController player)
    {
        base.UnInteract(player);
        rb.AddForce(player.transform.forward * player.GetStrength(), ForceMode.Impulse);
    }
}