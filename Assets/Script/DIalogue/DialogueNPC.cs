using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public DialogueType.dialogueType typeOfDialogue;
    public DialogueLine[] dialogueLines;

    [Header ("Setting Story (for main story only)")]
    public int currentMission;

    [Header ("Interaction Settings")]
    private bool playerInRange = false;
    public bool dialogueStarted = false;
    

    private void Start() {
        var data = DataPresistenceManager.instance.GetGameData();

        if (typeOfDialogue == DialogueType.dialogueType.MainStory)
        {
            if (data.MissionOrder != currentMission)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (playerInRange && dialogueStarted == false && Input.GetKeyDown(KeyCode.Space))
        {
            TryStartDialogue();
        }
        if (dialogueStarted == true && DialogueManager.instance.dialogueFinished == true)
        {
            Debug.Log("DEBUG: Dialogue Finished Condition Met. Calling HandleDialogueFinished.");

            dialogueStarted = false;
            HandleDialogueFinished();

            DialogueManager.instance.dialogueFinished = false;
        }
    }

    private void TryStartDialogue()
    {
        var Data = DataPresistenceManager.instance.GetGameData();

        if (typeOfDialogue == DialogueType.dialogueType.MainStory)
        {
            if (Data.MissionOrder == currentMission)
            {
                dialogueStarted = true;
                DialogueManager.instance.StartDialogue(dialogueLines);
            }
        }
        else
        {
            dialogueStarted = true;
            DialogueManager.instance.StartDialogue(dialogueLines);
        }
    }

    private void HandleDialogueFinished()
    {
        if (typeOfDialogue == DialogueType.dialogueType.MainStory)
        {
            if (MissionManager.Instance.ValidateMission(currentMission))
            {
                DataPresistenceManager.instance.SaveGame();
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        if (typeOfDialogue == DialogueType.dialogueType.MainStory)
        {
            data.MissionOrder = currentMission;
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
