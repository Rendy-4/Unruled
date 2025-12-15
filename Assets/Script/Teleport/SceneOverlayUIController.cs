using UnityEngine;
using TMPro;
using System.Collections;

public class SceneOverlayUIController : MonoBehaviour
{
    public static SceneOverlayUIController Instance;

    public CanvasGroup canvasGroup;
    public TextMeshProUGUI sceneText;

    public float fadeDuration = 1f;
    public float displayTime = 3f;

    private void Awake()
    {
        Instance = this;
        canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }

    public void PlaySceneText(
        string text,
        float fadeDurationOverride,
        float displayTimeOverride)
    {
        

        gameObject.SetActive(true);
        sceneText.text = text;

        StopAllCoroutines();
        StartCoroutine(SceneRoutine(
            fadeDurationOverride,
            displayTimeOverride
        ));
    }

    private IEnumerator SceneRoutine(float fade, float display)
    {
        yield return Fade(0, 1, fade);
        yield return new WaitForSeconds(display);
        yield return Fade(1, 0, fade);
        gameObject.SetActive(false);
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}
