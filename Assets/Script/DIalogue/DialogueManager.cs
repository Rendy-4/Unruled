using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("NPC Profiles")]
    public NPCExpressionProfile[] npcProfiles;
    private Dictionary<string, NPCExpressionProfile> profileLookup;
    private NPCExpressionProfile currentProfile;



    [Header("Links Components")]
    public TextMeshProUGUI nameBox;
    public TextMeshProUGUI textBox;
    public GameObject dialogueGameObject;
    public Image basePortrait;
    public Image ExpressionLayer;


    [Header("Fallback Portrait")]
    public Sprite defaultPortraitSprite;
    
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
            return;
        }
        profileLookup = new Dictionary<string, NPCExpressionProfile>();

        foreach(var profile in npcProfiles)
        {
            if (profile == null || string.IsNullOrEmpty(profile.npcName))
            continue;

            if(!profileLookup.ContainsKey(profile.npcName))
            profileLookup.Add(profile.npcName, profile);
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
                    basePortrait.sprite = currentProfile != null ? currentProfile.defaultPortrait : defaultPortraitSprite;
                    if (ExpressionLayer != null)
                    ExpressionLayer.sprite = null;
                
                    HideDialogueBox();
                }
            }
        }
    }

    public void SetNPCProfile(NPCExpressionProfile profile)
    {
        currentProfile = profile;

        if(basePortrait != null && profile != null)
        basePortrait.sprite = profile.defaultPortrait;
    }

    public void StartDialogue(DialogueData data)
{
    if (data == null || data.dialogueLines == null || data.dialogueLines.Length == 0)
    {
        Debug.LogWarning("DialogueData kosong / null");
        return;
    }

    dialogueGameObject.SetActive(true);
    dialogueFinished = false;

    dialogueLines = data.dialogueLines;
    currentLineIndex = 0;
    justStarted = true;

    ShowDialogueBox();
    typingCoroutine = StartCoroutine(TypeLine(dialogueLines[0]));
}



    IEnumerator TypeLine(DialogueLine line)
    {
        isTyping = true;
        textBox.text = "";
        nameBox.text = line.characterName;
        AutoAssignProfile(line.characterName);

        if (basePortrait != null)
        {
            basePortrait.sprite = line.portraitSprite != null
                ? line.portraitSprite
                : currentProfile != null
                    ? currentProfile.defaultPortrait
                    : defaultPortraitSprite;
        }
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
        basePortrait.sprite = line.portraitSprite != null
        ? line.portraitSprite
        : currentProfile != null
            ? currentProfile.defaultPortrait
            : defaultPortraitSprite;

        ApplyExpression(line.expression);
;
    }

    public void ForceCloseDialogue()
    {
        StopAllCoroutines();
        textBox.text = "";
        nameBox.text = "";
        
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
    
    private void AutoAssignProfile(string characterName)
    {
        if (profileLookup == null) return;

        if (profileLookup.TryGetValue(characterName, out var profile))
        {
            currentProfile = profile;

            if (basePortrait != null)
                basePortrait.sprite = profile.defaultPortrait;
        }
        else
        {
            currentProfile = null;

            if (basePortrait != null)
                basePortrait.sprite = defaultPortraitSprite;
        }
    }
    
    private void ApplyExpression(DialogueExpression expression)
    {
        if (ExpressionLayer == null || currentProfile == null)
        return;
        
        ExpressionLayer.sprite = currentProfile.GetExpression(expression);
    }

    
}
