using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneUIAnimator : MonoBehaviour
{
    public RectTransform panel;          // UI Panel cutscene (isi text)
    public float slideDuration = 0.4f;   // waktu animasi
    public float slideDistance = 200f;   // seberapa jauh slide dari bawah

    private Vector2 originalPosition;

    void Awake()
    {
        if (panel == null) 
            panel = GetComponent<RectTransform>();

        originalPosition = panel.anchoredPosition;
    }

    // Dipanggil oleh CutsceneManager saat cutscene dimulai
    public void PlayEnterAnimation()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(SlideIn());
    }

    // Dipanggil saat cutscene selesai
    public void PlayExitAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(SlideOut());
    }

    private System.Collections.IEnumerator SlideIn()
    {
        Vector2 startPos = originalPosition - new Vector2(0, slideDistance);
        Vector2 endPos = originalPosition;

        float t = 0f;
        panel.anchoredPosition = startPos;

        while (t < slideDuration)
        {
            t += Time.deltaTime;
            float lerp = t / slideDuration;

            panel.anchoredPosition = Vector2.Lerp(startPos, endPos, lerp);

            yield return null;
        }
    }

    private System.Collections.IEnumerator SlideOut()
    {
        Vector2 startPos = originalPosition;
        Vector2 endPos = originalPosition - new Vector2(0, slideDistance);

        float t = 0f;

        while (t < slideDuration)
        {
            t += Time.deltaTime;
            float lerp = t / slideDuration;

            panel.anchoredPosition = Vector2.Lerp(startPos, endPos, lerp);

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
