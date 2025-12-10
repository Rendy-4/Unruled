using UnityEngine;

public class DialogueNPC : MonoBehaviour, IDataPresistence
{
    [Header("Dialogue Settings")]
    public DialogueType.dialogueType typeOfDialogue;
    public DialogueLine[] dialogueLines;

    [Header ("Setting Story (for main story only)")]
    public int requiredStoryOrder = -1; 
    public int nextStoryOrder = -1;

    [Header ("Interaction Settings")]
    private bool playerInRange = false;
    public bool dialogueStrarted = false;

    private void Update()
    {
        if (playerInRange && dialogueStrarted == false && Input.GetKeyDown(KeyCode.Space))
        {
            TryStartDialogue();
        }
        if (dialogueStrarted == true && DialogueManager.instance.dialogueFinished == true)
        {
            dialogueStrarted = false;
            HandleDialogueFinished();
        }
    }

    private void TryStartDialogue()
    {
        var Data = DataPresistenceManager.instance.GetGameData();

        if (typeOfDialogue == DialogueType.dialogueType.MainStrory)
        {
            if (Data.MissionOrder == requiredStoryOrder)
            {
                dialogueStrarted = true;
                DialogueManager.instance.StartDialogue(dialogueLines);
            }
        }
        else
        {
            dialogueStrarted = true;
            DialogueManager.instance.StartDialogue(dialogueLines);
        }
    }

    private void HandleDialogueFinished()
    {
        if (typeOfDialogue == DialogueType.dialogueType.MainStrory)
        {
            var Data = DataPresistenceManager.instance.GetGameData();
            if (nextStoryOrder >= 0)
            {
                Data.MissionOrder = nextStoryOrder;
            }
        }
    }

    public void LoadData(GameData data)
    {
        if (typeOfDialogue == DialogueType.dialogueType.MainStrory)
        {
            if (data.MissionOrder == requiredStoryOrder)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void SaveData(ref GameData data)
    {
        if (typeOfDialogue == DialogueType.dialogueType.MainStrory)
        {
            if (nextStoryOrder >= 0 && dialogueStrarted == false && DialogueManager.instance.dialogueFinished == true)
            {
                data.MissionOrder = nextStoryOrder;
            }
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
