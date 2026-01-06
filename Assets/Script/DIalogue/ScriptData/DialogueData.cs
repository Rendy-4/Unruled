using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Header("Dialogue Content")]
    public DialogueLine[] dialogueLines;

    [Header("Mission Completion")]
    [Tooltip("-1 tidak menyelesaikan mission")]
    public int completeMission = -1;
}
