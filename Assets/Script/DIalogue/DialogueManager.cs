using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Linq.Expressions;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("Links Components")]
    public TextMeshProUGUI nameBox;
    public TextMeshProUGUI textBox;
    public Image portraitBox;
    public GameObject dialogueGameObject;
    public Image basePortrait;
    public Image ExpressionLayer;

    [Header("Optional: default sprite/ jika portrait tidak di isi /null)")]
    public Sprite defaultExpressionSprite; // optional fallback
    public Sprite defaultPortraitSprite;

    [Header("Ekspresi")]
    public Sprite defaultExpression;
    public Sprite happyExpression;
    public Sprite angryExpression;
    public Sprite sadExpression;
    public Sprite shockExpression;
    
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
        hiddenPosition = new Vector2(0, -900);

       if (dialoguePanel != null)
       dialoguePanel.anchoredPosition = hiddenPosition;

       if (ExpressionLayer != null)
       ExpressionLayer.sprite = null;
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
                    dialogueFinished = true;
                    textBox.text = "";
                    nameBox.text = "";

                    if (basePortrait != null)
                    basePortrait.sprite = defaultExpression;
                    if (ExpressionLayer != null)
                    ExpressionLayer.sprite = null;
                
                    HideDialogueBox();
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

        if (basePortrait != null)
            basePortrait.sprite = line.portraitSprite != null ? line.portraitSprite : defaultPortraitSprite;

            ApplyExpression(line.expression);

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

        if (basePortrait != null)
        basePortrait.sprite = line.portraitSprite != null ? line.portraitSprite : defaultPortraitSprite;
        ApplyExpression(line.expression);
;
    }

    public void ForceCloseDialogue()
    {
        StopAllCoroutines();
        textBox.text = "";
        nameBox.text = "";
        portraitBox.sprite = null;

        if (basePortrait != null)
        basePortrait.sprite = defaultPortraitSprite;
        if (ExpressionLayer != null)
        ExpressionLayer.sprite = null;

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
    
    private void ApplyExpression(DialogueExpression expression)
    {
        if (ExpressionLayer == null)
        {
            ExpressionLayer.sprite = null;
        }

        switch (expression)
        {
            case DialogueExpression.Happy:
            ExpressionLayer.sprite = happyExpression;
            break;
            case DialogueExpression.Sad:
                ExpressionLayer.sprite = sadExpression;
                break;
            case DialogueExpression.Angry:
                ExpressionLayer.sprite = angryExpression;
                break;
            case DialogueExpression.Shock:
                ExpressionLayer.sprite = shockExpression;
                break;
            default:
                ExpressionLayer.sprite = defaultExpressionSprite;
                break;
        }
    }
}
