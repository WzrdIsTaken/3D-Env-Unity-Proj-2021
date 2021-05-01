using UnityEngine;

// Manages visual fx animations
public class VfxCanvasManager : MonoBehaviour
{
    public enum Animation { FADE_OUT };

    Animator animator;

    const string FADE_OUT_TRIGGER = "FadeOut";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(Animation animation)
    {
        switch (animation)
        {
            case Animation.FADE_OUT:
                animator.SetTrigger(FADE_OUT_TRIGGER);
                break;
        }
    }
}