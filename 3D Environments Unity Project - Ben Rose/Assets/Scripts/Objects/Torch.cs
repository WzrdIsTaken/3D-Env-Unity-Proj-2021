using UnityEngine;
using System.Collections;

// Let there be light!
public class Torch : MonoBehaviour
{
    Light tourchLight;

    const float MIN_FLICKER_TIME = 0.75f, MAX_FLICKER_TIME = 1.5f;

    void Start()
    {
        tourchLight = GetComponentInChildren<Light>();

        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        float flickerTime = Random.Range(MIN_FLICKER_TIME, MAX_FLICKER_TIME);
        float timer = 0;

        float startIntesity = tourchLight.intensity;
        float endIntesity = Random.Range(0.5f, 1.0f);

        while (true)
        {
            tourchLight.intensity = Mathf.Lerp(startIntesity, endIntesity, timer / flickerTime);

            timer += Time.deltaTime;
            yield return null;

            if (timer > flickerTime) break;
        }

        StartCoroutine(Flicker());
    }
}
