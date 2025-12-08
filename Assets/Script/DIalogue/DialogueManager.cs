using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("Links Components")]
    public TextMeshProUGUI nameBox;
    public TextMeshProUGUI textBox;
    public Image portraitBox;
    public GameObject dialogueGameObject;

    [Header("Text Configuration")]
    public float textSpeed = 0.05f;

    [Header("Dialogue Status")]
    public bool isTyping = false;
    public bool dialogueFinished = true;

    [Header("Dialogue Data")]
    public DialogueLine[] dialogueLines;

    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private bool justStarted = false;

    [Header("Dialogue Animation")]
    public RectTransform dialoguePanel;
    public float slidespeed = 12f;

    private Vector2 hiddenPosition;
    private Vector2 shownPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        shownPosition = new Vector2(0, 172);
        hiddenPosition = new Vector2(0, -600);

        dialoguePanel.anchoredPosition = hiddenPosition;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (justStarted)
            {
                justStarted = false;
                return;
            }
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                ShowFullLine(dialogueLines[currentLineIndex]);
                isTyping = false;
            }
            else
            {
                currentLineIndex++;
                if (currentLineIndex < dialogueLines.Length)
                {
                    typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
                }
                else
                {
                    textBox.text = "";
                    nameBox.text = "";
                    portraitBox.sprite = null;
                    dialogueFinished = true;
                    dialogueGameObject.SetActive(false);
                }
            }
        }
    }

    public void StartDialogue(DialogueLine[] newLines)
    {
        dialogueGameObject.SetActive(true);
        dialogueFinished = false;

        dialogueLines = newLines;
        currentLineIndex = 0;
        justStarted = true;

        ShowDialogueBox();
        typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
    }

    IEnumerator TypeLine(DialogueLine line)
    {
        isTyping = true;
        textBox.text = "";
        nameBox.text = line.characterName;
        portraitBox.sprite = line.characterPortrait;
        foreach (char text in line.dialogueText)
        {
            textBox.text += text;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }

    private void ShowFullLine(DialogueLine line)
    {
        textBox.text = line.dialogueText;
        nameBox.text = line.characterName;
        portraitBox.sprite = line.characterPortrait;
    }

    public void ForceCloseDialogue()
    {
        StopAllCoroutines();
        textBox.text = "";
        nameBox.text = "";
        portraitBox.sprite = null;

        dialogueFinished = true;
        HideDialogueBox();
    }

    public void ShowDialogueBox()
    {
        StopAllCoroutines();
        StartCoroutine(SlideUI(dialoguePanel, shownPosition));
    }
    public void HideDialogueBox()
    {
        StopAllCoroutines();
        StartCoroutine(SlideUI(dialoguePanel, hiddenPosition));
    }

    IEnumerator SlideUI(RectTransform panel, Vector2 target)
    {
        while(Vector2.Distance(panel.anchoredPosition, target) > 0.1f)
        {
            panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, target, slidespeed * Time.deltaTime);
            yield return null;
        }
        panel.anchoredPosition = target;
    }
}
