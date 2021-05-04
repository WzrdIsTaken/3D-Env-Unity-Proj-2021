using UnityEngine;
using System.Collections;

// The actual portal you go through
public class Portal : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] float speed;
#pragma warning restore 649

    MeshRenderer portalTextureMesh;
    ParticleSystem portalParticleSystem;
    bool isActivated = false;

    void Start()
    {
        portalTextureMesh = GetComponentInChildren<MeshRenderer>();
        portalParticleSystem = GetComponentInChildren<ParticleSystem>();

        portalTextureMesh.gameObject.SetActive(false);
    }

    public void ActivatePortal()
    {
        portalTextureMesh.gameObject.SetActive(true);
        StartCoroutine(AnimatePortalTexture());
        portalParticleSystem.Play();

        isActivated = true;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && isActivated)
        {
            // A cooler animation could be nice, but not a big priority
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            playerController.SetDenyInput(true);
            FindObjectOfType<VfxCanvasManager>().PlayAnimation(VfxCanvasManager.Animation.FADE_OUT);
            FindObjectOfType<LevelManager>().ToggleEndPanel(true, playerController.GetInGodMode(), LevelManager.EndPanelState.LEVEL_END, 1);
        }
    }

    // I would have like to have used a shadergraph for this + make a cooler effect, but when I tried to import LWRP into the project every texture just broke.
    // Might figure out a solution to that later, but for now this effect is ok.
    IEnumerator AnimatePortalTexture(Vector2 oldSpeed=new Vector2())
    {
        Vector2 portalTextureScrollSpeed = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));
        float duration = Random.Range(1, 3);
        float timer = 0;
        bool changingDirection = oldSpeed != Vector2.zero;

        while (timer < duration)
        {
            Vector2 newOffset = changingDirection ? Vector2.Lerp(oldSpeed, portalTextureScrollSpeed, timer / duration) : portalTextureScrollSpeed;
            portalTextureMesh.material.SetTextureOffset("_MainTex", portalTextureMesh.material.mainTextureOffset + newOffset * Time.deltaTime);

            timer += Time.deltaTime;
            if (timer > duration && changingDirection)
            {
                timer = 0;
                changingDirection = false;
            }

            yield return null;
        }

        StartCoroutine(AnimatePortalTexture(portalTextureScrollSpeed));
    }
}