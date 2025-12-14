using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionUIController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public RectTransform panelTransform;
    public TextMeshProUGUI missionText;

    public float fadeDuration = 0.4f;
    public float slideDistance = 100f;
    public float displayTime = 3f;
    public float paddingVertical = 24f;
    private Vector2 originalPos;

    private void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        if (panelTransform == null) panelTransform = GetComponent<RectTransform>();

        originalPos = panelTransform.anchoredPosition;
        canvasGroup.alpha = 0;
    }

    

public void ShowMission(string text)
{
    missionText.text = text;

    // Paksa TMP hitung ulang
    missionText.ForceMeshUpdate();

    float textHeight = missionText.preferredHeight;

    // Resize panel sesuai teks + padding
    Vector2 size = panelTransform.sizeDelta;
    size.y = textHeight + paddingVertical * 2f;
    panelTransform.sizeDelta = size;

    StopAllCoroutines();
    StartCoroutine(AnimateMission());
}


    private System.Collections.IEnumerator AnimateMission()
    {
        panelTransform.anchoredPosition = originalPos - new Vector2(0, slideDistance);
        canvasGroup.alpha = 0;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float lerp = t / fadeDuration;

            canvasGroup.alpha = Mathf.Lerp(0, 1, lerp);
            panelTransform.anchoredPosition = Vector2.Lerp(
                originalPos - new Vector2(slideDistance, 0),
                originalPos,
                lerp
            );
            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }
    }
}
