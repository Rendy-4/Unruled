using System.Collections;
using UnityEngine;

public class MoverBase : MonoBehaviour
{
    public float speed = 2f;
    public bool IsMoving{get ; private set; }
    private Coroutine moveRoutine;
    public Animator animator;

    public IEnumerator MoveToWaypoints(Transform[] waypoints)
    {
        IsMoving = true;
        if(animator != null)
        animator.SetBool("isWalk",true);

       foreach (var wp in waypoints){
            while (Vector3.Distance(transform.position, wp.position) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    wp.position,
                    speed * Time.deltaTime
                );
                yield return null;
            }
        }
        if(animator != null)
        animator.SetBool("isWalk",false);
        IsMoving = false;
    }
    public void StartMove(MonoBehaviour owner, Transform[] waypoints)
    {
       StopMove();
       moveRoutine = owner.StartCoroutine(MoveToWaypoints(waypoints)); 
    }
    public void StopMove()
    {
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
    }
}
