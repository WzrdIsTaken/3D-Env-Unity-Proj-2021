using UnityEngine;

// Manages everything to do with the dungeon (ie, starting animations)
public class DungeonManager : MonoBehaviour
{
    Animator animator;

    const string OPEN_DOOR = "OpenDoor", CLOSE_DOOR = "CloseDoor", SWING_BLADES = "SwingBlades";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetTrigger(OPEN_DOOR);
        FindObjectOfType<CutsceneManager>().PlayCorridorCutscene();
    }

    public void CloseDoor()
    {
        animator.SetTrigger(CLOSE_DOOR);
    }

    public void SwingBlades()
    {
        animator.SetTrigger(SWING_BLADES);
    }
}