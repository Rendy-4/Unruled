
using UnityEngine;

public class NPCAnimatorDriver : MonoBehaviour
{
    public Animator animator;
    public float moveThreshold = 0.01f;

    Vector3 lastposition;

    void Start()
    {
        lastposition = transform.position;
    }

    void Update()
    {
        Vector3 delta = transform.position - lastposition;
        float moveX = -delta.x;
        animator.SetFloat("MoveX", moveX);
        animator.SetBool("isWalk", Mathf.Abs(moveX) > moveThreshold);
        lastposition = transform.position;

    }
}
