using UnityEngine;

public class NpcFace : MonoBehaviour
{
    public NPCExpressionProfile expressionProfile;

    public void Apply()
    {
        if(expressionProfile != null)
            DialogueManager.instance.SetNPCProfile(expressionProfile);
    }
}
