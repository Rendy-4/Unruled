using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Header("Dialogue Content")]
    public DialogueLine[] dialogueLines;

    [Header("Mission Completion")]
    [Tooltip("Dialog ini HANYA aktif saat currentMission == angka ini")]
    public int missionToComplete;

}
