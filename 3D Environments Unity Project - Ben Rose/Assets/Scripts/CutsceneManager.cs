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

    PlayableDirector director;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void PlayCorridorCutscene()
    {
        void SetDenyInputOnPlayer(bool value)
        {
            FindObjectOfType<PlayerController>().SetDenyInput(value);
        }
        
        StartCoroutine(PlayCutscene(corridorCutscene, SetDenyInputOnPlayer));
    }

    IEnumerator PlayCutscene(CameraCutscenePair cutscene, Action<bool> specificAction = null)
    {
        GameObject mainCamera = Camera.main.gameObject;
        mainCamera.SetActive(false);
        specificAction?.Invoke(true);

        cutscene.camera.SetActive(true);
        director.Play(cutscene.playable);

        yield return new WaitForSeconds((float)director.duration);

        specificAction?.Invoke(false);
        cutscene.camera.SetActive(false);
        mainCamera.SetActive(true);
    }

    [Serializable]
    struct CameraCutscenePair
    {
        public GameObject camera;
        public PlayableAsset playable;

        public CameraCutscenePair(GameObject _camera, PlayableAsset _playable)
        {
            camera = _camera;
            playable = _playable;
        }
    }
}