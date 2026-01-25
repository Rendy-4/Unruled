using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public PlayerMovement3D playerMovement; // drag di inspector
    public Animator animator;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public string forceWalkBool = "forceWalk";

    public bool IsMoving { get; private set; }

    public void StartMove(Transform[] waypoints)
    {
        if (IsMoving || waypoints == null || waypoints.Length == 0)
            return;

        StartCoroutine(AutoMoveRoutine(waypoints));
    }

    IEnumerator AutoMoveRoutine(Transform[] waypoints)
    {
        IsMoving = true;

        // 🔒 LOCK PLAYER CONTROL (AMAN)
        if (playerMovement)
            playerMovement.enabled = false;

        if (rb)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        if (animator)
            animator.SetBool(forceWalkBool, true);

        foreach (Transform target in waypoints)
        {
            if (!target) continue;

            Vector3 targetPos = target.position;
            targetPos.y = transform.position.y;

            while (Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPos,
                    moveSpeed * Time.deltaTime
                );
                yield return null;
            }
        }

        // 🔓 UNLOCK PLAYER CONTROL
        if (animator)
            animator.SetBool(forceWalkBool, false);

        if (rb)
            rb.isKinematic = false;

        if (playerMovement)
            playerMovement.enabled = true;

        IsMoving = false;
    }
}
