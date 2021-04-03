using UnityEngine;
using TMPro;

// Base class for all thing interactable
public class InteractableObject : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] string objectName;
    [SerializeField] TypeOfInteraction typeOfInteraction;
#pragma warning restore 649

    enum TypeOfInteraction { PickUp, Open, Insert }

    bool isInteractable = true;
    const float FADE_TIME = 0.1f;

    public virtual void DisplayMessage(bool isInRange, PlayerController player)
    {
        TMP_Text interactionText = player.GetInteractionText();

        interactionText.text = "Press " + player.GetKey("Interaction") + " to " + typeOfInteraction.ToString().ToLower() + " the " + objectName;
        FadeInteractionText(isInRange, interactionText);
    }

    protected void FadeInteractionText(bool isInRange, TMP_Text interactionText)
    {
        interactionText.CrossFadeAlpha(isInRange ? 1.0f : 0.0f, FADE_TIME, false);
    }

    // The first interaction (eg: picking something up)
    public virtual void Interact(PlayerController player)
    {
    }

    // Stop interacting with an object (eg: putting something down)
    public virtual void UnInteract(PlayerController player)
    {
    }

    // The action once the object has been interacted with (eg: throwing the rock)
    public virtual void Action(PlayerController player)
    {
    }

    public bool GetInteractableState()
    {
        return isInteractable;
    }

    protected void SetInteractableState(bool _isInteractable)
    {
        isInteractable = _isInteractable;
    }
}