using UnityEngine;
using UnityEngine.Playables;
using System;
using System.Collections;

// Does cool cutscene stuff

/** 
 * It would be possible to just have a single cutscene camera, but having the pair means that things are clearer in the inspector imo. 
 * Maybe not in this project, but in a larger one with more complex cameras I think clarity would take precedent. 
 * Eg: Instead of having to play the animation that sets the camera settings, you could instantly see that its a wide angle shot with a specific target texture.
**/

public class CutsceneManager : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] CameraCutscenePair corridorCutscene, mainRoomCutscene;
#pragma warning restore 649

    public void PlayCorridorCutscene()
    {
        void SetDenyInputOnPlayer(bool value)
        {
            FindObjectOfType<PlayerController>().SetDenyInput(value);
        }
        
        StartCoroutine(PlayCutscene(corridorCutscene, SetDenyInputOnPlayer));
    }

    public void PlayMainRoomCutscene()
    {
        void ForcePlayerMoveForward(bool value)
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.SetDenyInput(value);

            if (value) StartCoroutine(playerController.ForceMovement(new Vector3(0, 0.1f, 30), new Quaternion(0, 0, 0, 0), 1, 5));
        }

        StartCoroutine(PlayCutscene(mainRoomCutscene, ForcePlayerMoveForward));
    }

    IEnumerator PlayCutscene(CameraCutscenePair cutscene, Action<bool> specificAction = null)
    {
        GameObject mainCamera = Camera.main.gameObject;
        mainCamera.SetActive(false);
        specificAction?.Invoke(true);

        cutscene.camera.SetActive(true);
        cutscene.playableDirector.time = 0;
        cutscene.playableDirector.Play();

        yield return new WaitForSeconds((float)cutscene.playableDirector.duration);

        specificAction?.Invoke(false);
        cutscene.camera.SetActive(false);
        mainCamera.SetActive(true);
    }

    [Serializable]
    struct CameraCutscenePair
    {
        public GameObject camera;
        public PlayableDirector playableDirector;

        public CameraCutscenePair(GameObject _camera, PlayableDirector _playableDirector)
        {
            camera = _camera;
            playableDirector = _playableDirector;
        }
    }
}