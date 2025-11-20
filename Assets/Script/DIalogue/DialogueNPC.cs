using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public DialogueLine[] dialogueLines;

    private bool playerInRange = false;
    public bool dialogueStrarted = false;

    private void Update()
    {
        if (playerInRange && dialogueStrarted == false && (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")))
        {
            DialogueManager.instance.StartDialogue(dialogueLines);
            dialogueStrarted = true;
        }
        if (dialogueStrarted == true && DialogueManager.instance.dialogueFinished == true)
        {
            dialogueStrarted = false;
        }
    }    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
