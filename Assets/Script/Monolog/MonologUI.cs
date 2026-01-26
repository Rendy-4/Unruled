using UnityEngine;
using System.Collections;
using TMPro;
public class MonologUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI bodyText;
    public float typingSpeed = 0.04f;
    string currenttext;
    bool isTyping;
    Coroutine typingCouroutine;
    [Header("Auto Hide")]
    public float autoHideDelay = 1.5f;
    public float fadeDuration = 0.4f;
    bool isAutoHiding;

    public CanvasGroup canvasGroup;

    void Awake()
    {
        panel.SetActive(false);
        canvasGroup.alpha = 0f;
    }

    void Update()
    {
        if(!panel.activeSelf)
        return;
        if(!isTyping)
        return;
    }

    public void Play(string text)
    {
        currenttext = text;
        isTyping = false;
        isAutoHiding = false;

        if(typingCouroutine != null)
        StopCoroutine(typingCouroutine);

        bodyText.text = "";
        canvasGroup.alpha = 1f;
        panel.SetActive(true);

            typingCouroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        bodyText.text = "";

        foreach (char c in currenttext)
        {
            bodyText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        StartAutoHide();
    }

    public void Hide()
    {
        panel.SetActive(false);
        canvasGroup.alpha = 1f;
    }
    IEnumerator AutoHide()
    {
        isAutoHiding = true;

        yield return new WaitForSeconds(autoHideDelay);

        float time = 0f;
        float startAlpha = canvasGroup.alpha;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
        panel.SetActive(false);
        isAutoHiding = false;
    }

    void StartAutoHide()
    {
        if(!isAutoHiding)
        StartCoroutine(AutoHide());
    }

    public bool IsTyping()
    {
        return isTyping;
    }
}
