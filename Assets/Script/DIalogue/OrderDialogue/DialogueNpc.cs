using UnityEngine;
using System;

public class DialogueNPC : MonoBehaviour
{
    public static Action<DialogueData> OnDialogueFinished;
    [Header("Dialogue Settings")]
    public DialogueData dialogueData;

    [Header ("Interaction Settings")]
    private bool playerInRange = false;
    private bool dialogueStarted = false;
    private bool finishedInvoked = false;
    private NpcFace npcFace;

    void Awake()
    {
        npcFace = GetComponent<NpcFace>();
    }

    private void Update()
    {
        if (playerInRange && !dialogueStarted && Input.GetKeyDown(KeyCode.Space)) //Dialog Baru
        {

            if(!CanStartDialogue())
            return;

            finishedInvoked = false;
            dialogueStarted = true;

            npcFace?.Apply();
            DialogueManager.instance.StartDialogue(dialogueData);
        }

        if (dialogueStarted && DialogueManager.instance.dialogueFinished && !finishedInvoked) //Dialog Selesai
        {   
            finishedInvoked = true;
            dialogueStarted = false;
            
            OnDialogueFinished?.Invoke(dialogueData);
        }
    }

    bool CanStartDialogue()
    {
        if (dialogueData == null)
        return false;

        //Dialog mission hanya boleh jika mission masih sesuai
        if (dialogueData.completeMission >= 0)
        {
            return MissionManager.Instance.currentMission == dialogueData.completeMission;
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
            DialogueManager.instance.ForceCloseDialogue();
        }
    }
}
