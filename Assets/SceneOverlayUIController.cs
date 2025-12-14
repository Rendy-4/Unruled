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

    public void PlaySceneText(string text)
    {
        gameObject.SetActive(true);
        sceneText.text = text;
        StopAllCoroutines();
        StartCoroutine(SceneRoutine());
    }

    private IEnumerator SceneRoutine()
    {
        yield return Fade(0, 1);
        yield return new WaitForSeconds(displayTime);
        yield return Fade(1, 0);
        gameObject.SetActive(false);
    }

    private IEnumerator Fade(float from, float to)
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}
