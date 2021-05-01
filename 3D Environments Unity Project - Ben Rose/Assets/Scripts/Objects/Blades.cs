using UnityEngine;
using System.Collections;

// Some logic so, amongst other things, ensure that the player doesn't accelerate to mach 1 when they get hit

/** 
 * Basically the blade collisions are happening too fast, so the easiest way to solve the problem imo is just to have this 'box'
 * which if the player is in when the blades go off then it acts as they got hit by them.
**/

public class Blades : MonoBehaviour
{
    BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    public IEnumerator EnableBladeCollider(float duration)
    {
        boxCollider.enabled = true;

        yield return new WaitForSeconds(duration);

        boxCollider.enabled = false;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<PlayerController>().TakeDamage(int.MaxValue);
            print("called!");
        }
    }
}