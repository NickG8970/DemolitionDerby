using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private Image black;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private AnimationCurve fadeCurve;

    void Start()
    {
        StartCoroutine(WaitForFadeIn());
    }

    public void FadeTo(string sceneName)
    {
        StartCoroutine(WaitForFadeTo(sceneName));
    }

    IEnumerator WaitForFadeTo(string sceneName)
    {
        float t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            Color colour = black.color;
            colour.a = fadeCurve.Evaluate(t);
            black.color = colour;
            yield return 0;
        }

        SceneManager.LoadScene(sceneName);
        yield return null;
    }

    IEnumerator WaitForFadeIn()
    {
        float t = fadeTime;

        while (t > 0)
        {
            t -= Time.deltaTime;
            Color colour = black.color;
            colour.a = fadeCurve.Evaluate(t);
            black.color = colour;
            yield return 0;
        }

        yield return null;
    }
}