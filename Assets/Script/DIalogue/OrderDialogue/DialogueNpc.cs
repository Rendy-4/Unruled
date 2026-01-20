using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public DialogueData dialogueData;

    [Header ("Interaction Settings")]
    private bool playerInRange = false;
    private bool dialogueStarted = false;
    private NpcFace npcFace;
    private NpcMissionMover missionmover;

    void Awake()
    {
        npcFace = GetComponent<NpcFace>();
        missionmover = GetComponent<NpcMissionMover>();
    }
    void OnEnable()
    {
        DialogueManager.onDialogueFinished += HandleDialogueFinished;
    }
    void OnDisable()
    {
        DialogueManager.onDialogueFinished -= HandleDialogueFinished;
    }
    private void Update()
    {
        
            if(!playerInRange)
            return;
            if(dialogueStarted)
            return;

        
        if (missionmover != null & missionmover.IsMoving())
        return;
        


            if(!Input.GetKeyDown(KeyCode.Space))
            return;
            
            if(DialogueManager.instance == null)
            return;

            if(!CanStartDialogue())
            return;

            dialogueStarted = true;
            npcFace?.Apply();
            DialogueManager.instance.StartDialogue(dialogueData);
         
    }

    private void HandleDialogueFinished(DialogueData data)
    {
        dialogueStarted = false;
    }

    bool CanStartDialogue()
    {   
        if (dialogueData == null)
        return false;

        if (dialogueData.missionToComplete
 >= 0)
        {
            return MissionManager.Instance.currentMission == dialogueData.missionToComplete
;
        }
        return true;
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
            dialogueStarted = false;
            DialogueManager.instance.ForceCloseDialogue();
        }
    }
}
