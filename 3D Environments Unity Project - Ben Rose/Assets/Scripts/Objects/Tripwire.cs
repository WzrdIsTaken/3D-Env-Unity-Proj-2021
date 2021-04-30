using UnityEngine;

// Don't trip
public class Tripwire : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(FindObjectOfType<DungeonManager>().StartAnimation(DungeonManager.Animation.SWING_BLADES));
    }
}
