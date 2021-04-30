using UnityEngine;
using System.Collections;

// Manages everything to do with the dungeon (ie, starting animations)
public class DungeonManager : MonoBehaviour
{
    [HideInInspector] public enum Animation { OPEN_DOOR, CLOSE_DOOR, SWING_BLADES };

    Animator animator;

    const string OPEN_DOOR_TRIGGER = "OpenDoor", CLOSE_DOOR_TRIGGER = "CloseDoor", SWING_BLADES_TRIGGER = "SwingBlades";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator StartAnimation(Animation animation, float startDelay=0)
    {
        yield return new WaitForSeconds(startDelay);

        switch (animation)
        {
            case Animation.OPEN_DOOR:
                OpenDoor();
                break;
            case Animation.CLOSE_DOOR:
                CloseDoor();
                break;
            case Animation.SWING_BLADES:
                SwingBlades();
                break;
        }
    }

    void OpenDoor()
    {
        animator.SetTrigger(OPEN_DOOR_TRIGGER);
        FindObjectOfType<CutsceneManager>().PlayCorridorCutscene();
    }

    void CloseDoor()
    {
        animator.SetTrigger(CLOSE_DOOR_TRIGGER);
    }

    void SwingBlades()
    {
        animator.SetTrigger(SWING_BLADES_TRIGGER);
    }
}