using UnityEngine;
using System;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

// Does cool level ui related stuff
public class LevelManager : MonoBehaviour
{
    public enum EndPanelState { DEATH, LEVEL_END };

#pragma warning disable 649
    [SerializeField] GameObject startPanel, endPanel;
#pragma warning restore 649

    float startTime;

    const float FADE_TIME = 0.25f;

    void Start()
    {
        ToggleStartPanel(true);
    }

    public void ToggleStartPanel(bool enable)
    {
        StartCoroutine(FadeUI(enable, FADE_TIME, startPanel));

        FindObjectOfType<PlayerController>().SetDenyInput(enable);
        if (!enable) startTime = Time.time;
    }

    public void ToggleEndPanel(bool enable, bool playerInGodmode, EndPanelState state, float fadeDelay=0)
    {
        StartCoroutine(FadeUI(enable, FADE_TIME, endPanel, fadeDelay));

        TimeSpan totalTime = TimeSpan.FromSeconds(Time.time - startTime);
        string time = totalTime.ToString("mm':'ss");

        endPanel.transform.Find("EndText").GetComponent<TMP_Text>().text = state == EndPanelState.DEATH ? "You died!" : "Level complete!";
        endPanel.transform.Find("CommentText").GetComponent<TMP_Text>().text = state == EndPanelState.DEATH ? "Ouch!" : "Nice job!";
        endPanel.transform.Find("TimerText").GetComponent<TMP_Text>().text = state == EndPanelState.DEATH ? "It took you " + time + " to get sliced in half..." : "" + "It took you " + time + " to reach the end!" 
                                                                                                            + (playerInGodmode ? " I'll ignore the fact you were in godmode..." : string.Empty);

        endPanel.transform.Find("RestartButton").GetComponentInChildren<TMP_Text>().text = state == EndPanelState.DEATH ? "Restart" : "Play again";
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        Debug.Log("The game would exit here");
#else
        Application.Quit();
#endif
    }

    IEnumerator FadeUI(bool fadeIn, float fadeTime, GameObject parent, float fadeDelay=0)
    {
        yield return new WaitForSeconds(fadeDelay);

        if (fadeIn) parent.SetActive(true);

        CanvasGroup canvasGroup = parent.GetComponent<CanvasGroup>();
        float timer = 0;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(fadeIn ? 0 : 1, fadeIn ? 1 : 0, timer / fadeTime);

            yield return null;
        }

        canvasGroup.alpha = fadeIn ? 1 : 0;
        if (!fadeIn) parent.SetActive(false);
    }
}