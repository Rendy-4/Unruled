using UnityEngine;

[CreateAssetMenu(
    fileName = "NPCExpressionProfile",
    menuName = "Dialogue/NPC Expression Profile"
)]
public class NPCExpressionProfile : ScriptableObject
{
    public string npcName;
    public Sprite defaultPortrait;

    [Header("Expression Sprites")]
    public Sprite defaultExpression;
    public Sprite happy;
    public Sprite angry;
    public Sprite sad;
    public Sprite shock;

    [Header("Expression Position Offset")]
    public Vector2 expressionOffset;

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
