using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [Header ("Expression Profile")]
    public NPCExpressionProfile expressionProfile;

    [Header("Dialogue Settings")]
    public DialogueType.dialogueType typeOfDialogue;
    public DialogueData dialogueData;

    [Header ("Interaction Settings")]
    private bool playerInRange = false;
    public bool dialogueStarted = false;
 
    private void Update()
    {
        if (playerInRange && !dialogueStarted && Input.GetKeyDown(KeyCode.Space))
        {
            dialogueStarted = true;
            DialogueManager.instance.SetNPCProfile(expressionProfile);
            DialogueManager.instance.StartDialogue(dialogueData);
        }

        if (dialogueStarted && DialogueManager.instance.dialogueFinished)
        {
            dialogueStarted = false;
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
