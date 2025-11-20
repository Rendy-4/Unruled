using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public Image characterAvatar;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> dialogueLines;

    public bool IsOpen;
    public float typingSpeed = 0.25f;
    public Animator animator;
    private void Start() {
        if (instance == null)
        {
            instance = this;
        }

        dialogueLines = new Queue<DialogueLine>();
        IsOpen = false;
        animator.SetBool("IsOpen", false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        IsOpen = true;
        animator.SetBool("IsOpen", true);
        dialogueLines.Clear();

        foreach (DialogueLine line in dialogue.lines)
        {
            dialogueLines.Enqueue(line);
        }
        DisplayNextLine();
    }

    private void Update() {
        if (IsOpen && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    public void DisplayNextLine()
    {
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = dialogueLines.Dequeue();
        characterAvatar.sprite = currentLine.character.avatar;
        characterNameText.text = currentLine.character.name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        IsOpen = false;
        animator.SetBool("IsOpen", false);
    }
}

