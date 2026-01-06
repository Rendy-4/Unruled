using UnityEngine;

[System.Serializable]
public class CutsceneLine
{
    [TextArea(2, 5)]
    public string text;
    public float duration = 2f; 
}
