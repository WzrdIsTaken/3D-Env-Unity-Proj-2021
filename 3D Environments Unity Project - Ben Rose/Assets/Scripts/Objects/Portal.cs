using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// Handles all things portal: Inserting the key, starting the shader and exiting the level
public class Portal : InteractableObject
{
    public override void DisplayMessage(bool isInRange, PlayerController player)
    {
        TMP_Text interactionText = player.GetInteractionText();

        interactionText.text = player.GetCurrentlyHoldingObject() is PowerCube ? "Press " + player.GetKey("Action") + " to insert the powercube" : "Insert the powercube to activate the portal";
        FadeInteractionText(isInRange, interactionText);
    }

    public void StartPortal()
    {
        print("Portal activated");

        // Todo: Start the portal 
        // - Inserting key / removing it from player hand
        // - Shader starting
        // - ^ Cool cutscene ^

        isInteractable = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !isInteractable)
        {
            // Todo: Cool animation
            SceneManager.LoadScene("Wherever we go next");
        }
    }
}
