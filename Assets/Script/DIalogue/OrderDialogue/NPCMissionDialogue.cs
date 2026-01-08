using UnityEngine;

public class NPCMissionDialogue : MonoBehaviour
{
    [System.Serializable]
    public class MissionDialogue
    {
        public int missionIndex;
        public DialogueData dialogue;
    }

    [Header("Dialogue Per Mission")]
    public MissionDialogue[] dialogues;
    private DialogueNPC dialogueNPC;

    void Awake()
    {
        dialogueNPC = GetComponent<DialogueNPC>();
    }

    void OnEnable()
    {
        MissionManager.OnMissionUpdated += UpdateDialogue;
    }
    void OnDisable()
    {
        MissionManager.OnMissionUpdated -= UpdateDialogue;
    }
    void Start()
    {
        UpdateDialogue();
    }
    void UpdateDialogue()
    {
        int mission = MissionManager.Instance.currentMission;
        dialogueNPC.dialogueData = null;

        if (dialogueNPC.dialogueData == null)
        {
        Debug.Log($"[NPCMissionDialogue] No dialogue for mission {mission} on {gameObject.name}");
        }


        foreach(var d in dialogues)
        {
            if(d.missionIndex == mission)
            {
                dialogueNPC.dialogueData = d.dialogue;
                return;
            }
        }
    }
}
