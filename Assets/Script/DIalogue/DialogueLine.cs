using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    [TextArea(3, 10)]
    public string dialogueText;
    public Sprite portraitSprite;
    public DialogueExpression expression = DialogueExpression.Default;
}
public enum DialogueExpression
{
    Default,
    Happy,
    Sad,
    Angry,
    Shock,
}
