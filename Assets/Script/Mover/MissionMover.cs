using UnityEngine;
using System.Collections;

public class MissionMover : MonoBehaviour
{
    [Header ("Base Movement")]
    public float moveSpeed = 2f;

    protected bool isMoving;
    protected float lockedY;
    protected bool lockY;
    protected Animator animator;

    public bool IsMoving => isMoving;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected IEnumerator MoveThroughWaypoints(Transform[] waypoints)
    {
        isMoving = true;
        lockedY = transform.position.y;
        lockY = true;

        foreach (Transform target in waypoints)
        {
            if (target == null)
            continue;

            while (Vector3.Distance(transform.position, target.position) > 0.05f)
            {
                Vector3 pos = target.position;
                pos.y = lockedY;

                transform.position = Vector3.MoveTowards(
                    transform.position,
                    pos,
                    moveSpeed * Time.deltaTime
                );
                yield return null;
            }
        } 
        lockY = false;
        isMoving = false;  
    }
    protected void LateUpdate()
    {
        if (!lockY)
        return;

        Vector3 pos = transform.position;
        pos.y = lockedY;
        transform.position = pos;
    }
}
