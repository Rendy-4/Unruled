using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite avatar;
}
[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}
[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> lines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        Debug.Log("Trigger ditabrak oleh objek: " + collision.name);
        if(collision.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }
}
    

