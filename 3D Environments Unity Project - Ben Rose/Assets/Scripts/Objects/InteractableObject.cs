using UnityEngine;
using TMPro;

// Base class for all thing interactable
public class InteractableObject : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] string objectName;
    [SerializeField] TypeOfInteraction typeOfInteraction;
#pragma warning restore 649

    enum TypeOfInteraction { PickUp, Open }
    const float FADE_TIME = 0.1f;

    public void DisplayMessage(bool isInRange, TMP_Text interactionText)
    {
        interactionText.text = "Press P to " + typeOfInteraction.ToString().ToLower() + " " + objectName;
        interactionText.CrossFadeAlpha(isInRange ? 1.0f : 0.0f, FADE_TIME, false);
    }

    // The first interaction (eg: picking something up)
    public virtual void Interact(PlayerController player)
    {
    }

    // Putting down an object
    public virtual void UnInteract(PlayerController player)
    {
    }

    // The action once the object has been interacted with (eg: throwing the rock)
    public virtual void Action(PlayerController player)
    {
    }
}