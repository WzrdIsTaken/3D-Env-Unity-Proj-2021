using UnityEngine;
using UnityEngine.SceneManagement;

// The actual portal you go through
public class Portal : MonoBehaviour
{
    bool isActivated = false;

    public void ActivatePortal()
    {
        // - Shader starting
        // - ^ Cool cutscene ^

        isActivated = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && isActivated)
        {
            // Todo: Cool animation
            SceneManager.LoadScene("Wherever we go next");
        }
    }
}