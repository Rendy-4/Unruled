using UnityEngine;

[CreateAssetMenu(
    fileName = "NPCExpressionProfile",
    menuName = "Dialogue/NPC Expression Profile"
)]
public class NPCExpressionProfile : ScriptableObject
{
    public string npcName;

    public Sprite defaultPortrait;

    public Sprite defaultExpression;
    public Sprite happy;
    public Sprite angry;
    public Sprite sad;
    public Sprite shock;

    public Sprite GetExpression(DialogueExpression expression)
    {
        switch (expression)
        {
            case DialogueExpression.Happy: return happy;
            case DialogueExpression.Angry: return angry;
            case DialogueExpression.Sad: return sad;
            case DialogueExpression.Shock: return shock;
            default: return defaultExpression;
        }
    }
}
