using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public DialogueLine[] dialogueLines;

    private bool playerInRange = false;
    public bool dialogueStrarted = false;

    private void Update()
    {
        if (playerInRange && dialogueStrarted == false && Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.instance.StartDialogue(dialogueLines);
            dialogueStrarted = true;
        }
        if (dialogueStrarted == true && DialogueManager.instance.dialogueFinished == true)
        {
            dialogueStrarted = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player out of range");
            playerInRange = false;
            DialogueManager.instance.ForceCloseDialogue();
        }
    }
}
