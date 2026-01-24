using UnityEngine;

public class NPCFlip : MonoBehaviour
{
    public Animator animator;
    public Transform visual;

    void LateUpdate()
    {
        float moveX = animator.GetFloat("MoveX");

        if(Mathf.Abs(moveX) < 0.01f)
        return;

        Vector3 scale = visual.localScale;
        if (moveX > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else 
        {
          scale.x = -Mathf.Abs(scale.x);  
        }
        visual.localScale = scale;
    }
}
