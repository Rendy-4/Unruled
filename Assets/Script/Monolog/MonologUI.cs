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

    public CanvasGroup canvasGroup;

    void Awake()
    {
        Hide();
    }

    void Update()
    {
        if(!panel.activeSelf)
        return;
        if(!isTyping)
        return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Skip();
        }
    }

    public void Play(string text)
    {
        currenttext = text;
        isTyping = false;

        bodyText.text = "";
        canvasGroup.alpha = 1f;
        panel.SetActive(true);

        if(typingCouroutine != null)
            StopCoroutine(typingCouroutine);

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
        StartCoroutine(AutoHide());
    }

    void Skip()
    {
        if(!isTyping)
        return;

        StopCoroutine(typingCouroutine);
        bodyText.text = currenttext;
        isTyping = false;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
    IEnumerator AutoHide()
    {
        yield return new WaitForSeconds(autoHideDelay);

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            yield return null;
        }
    }

    public bool IsTyping()
    {
        return isTyping;
    }
}
