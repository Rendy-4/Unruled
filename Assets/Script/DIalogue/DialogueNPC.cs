using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [Header ("Expression Profile")]
    public NPCExpressionProfile expressionProfile;

    [Header("Dialogue Settings")]
    public DialogueType.dialogueType typeOfDialogue;
    public DialogueData dialogueData;

    [Header ("Setting Story (for main story only)")]
    public int currentMission;

    [Header ("Interaction Settings")]
    private bool playerInRange = false;
    public bool dialogueStarted = false;

    private Collider npcCollider;
    private void Awake()
    {
        npcCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        // Mengikuti ke event mission update supaya bisa re-check ketika mission berubah
        MissionManager.OnMissionUpdated += CheckMissionCondition;
        MissionManager.ForceRefreshNPC += TryReactivate;

    }

    private void OnDisable()
    {
        MissionManager.OnMissionUpdated -= CheckMissionCondition;
        MissionManager.ForceRefreshNPC -= TryReactivate;

    }

   private void CheckMissionCondition()
{
    if (DataPresistenceManager.instance == null)
    {
        Debug.LogWarning("DataPresistenceManager.instance is null!");
        return; // keluar dulu
    }

    var data = DataPresistenceManager.instance.GetGameData();
    if (data == null)
    {
        Debug.LogWarning("GameData is null!");
        return;
    }

    bool shouldAppear = (data.MissionOrder == currentMission);
    if (npcCollider != null)
        npcCollider.enabled = shouldAppear;

    foreach (var r in GetComponentsInChildren<Renderer>())
        r.enabled = shouldAppear;
}



    

    private void Update()
    {
        if (playerInRange && dialogueStarted == false && Input.GetKeyDown(KeyCode.Space))
        {
            TryStartDialogue();
        }
        if (dialogueStarted == true && DialogueManager.instance.dialogueFinished == true)
        {

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
                DialogueManager.instance.SetNPCProfile(expressionProfile);
                DialogueManager.instance.StartDialogue(dialogueData);
            }
        }
        else
        {
            dialogueStarted = true;
            DialogueManager.instance.SetNPCProfile(expressionProfile);
            DialogueManager.instance.StartDialogue(dialogueData);
        }
    }

    public void HandleDialogueFinished()
    {
        if (typeOfDialogue == DialogueType.dialogueType.MainStory)
        {
            if (MissionManager.Instance.ValidateMission(currentMission))
            {
                
                DataPresistenceManager.instance.SaveGame();

                npcCollider.enabled = false;
                foreach (var r in GetComponentsInChildren<Renderer>())
                r.enabled = false;

            }
        }
    }
    private void TryReactivate()
    {
        gameObject.SetActive(true);
        Invoke(nameof(CheckMissionCondition), 0.1f);
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
