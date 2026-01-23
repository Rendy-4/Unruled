using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public MonoBehaviour playerInput;   
    public Animator animator;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public string forceWalkBool = "forceWalk";

    public bool IsMoving { get; private set; }

    public void StartMove(Transform[] waypoints)
    {
        if(IsMoving)
        return;
        StartCoroutine(AutoMoveRoutine(waypoints));
    }

    IEnumerator AutoMoveRoutine(Transform[] waypoints)
    {
        IsMoving = true;

        if(playerInput)
        playerInput.enabled = false;
        if(rb)
        rb.isKinematic = true;
        if(animator)
        animator.SetBool(forceWalkBool, true);

        foreach (Transform target in waypoints)
        {

            if(target == null)
            continue;

                while (Vector3.Distance(transform.position, target.position) > 0.05f)
            {
            Vector3 pos = target.position;
            pos.y = transform.position.y;

            transform.position = Vector3.MoveTowards(
                transform.position,
                pos,
                moveSpeed * Time.deltaTime
                );
                yield return null;
            }
        } 
        if(animator)
        animator.SetBool(forceWalkBool, false); 
        if(rb)
        rb.isKinematic = false;
        if(playerInput)
        playerInput.enabled = true;

        IsMoving = false;
    }
}
