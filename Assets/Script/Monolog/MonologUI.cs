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

    void Update()
    {
        if (panel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            Skip();
        }
    }

    public void Play(string text)
    {
        currenttext = text;
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

    public bool IsTyping()
    {
        return isTyping;
    }
}
