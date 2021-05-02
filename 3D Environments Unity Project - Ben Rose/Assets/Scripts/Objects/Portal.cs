using UnityEngine;

// The actual portal you go through
public class Portal : MonoBehaviour
{
    MeshRenderer portalTextureMesh;
    bool isActivated = false;

    void Start()
    {
        portalTextureMesh = GetComponentInChildren<MeshRenderer>();
        portalTextureMesh.gameObject.SetActive(false);
    }

    public void ActivatePortal()
    {
        portalTextureMesh.gameObject.SetActive(true);

        isActivated = true;
    }

    void OnTriggerEnter(Collider collision)
    {
        print("called");

        if (collision.gameObject.CompareTag("Player") && isActivated)
        {
            // A cooler animation could be nice, but not a big priority
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            playerController.SetDenyInput(true);
            FindObjectOfType<VfxCanvasManager>().PlayAnimation(VfxCanvasManager.Animation.FADE_OUT);
            FindObjectOfType<LevelManager>().ToggleEndPanel(true, playerController.GetInGodMode(), LevelManager.EndPanelState.LEVEL_END, 1);
        }
    }
}