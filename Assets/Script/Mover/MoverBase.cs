using System.Collections;
using UnityEngine;

public class MoverBase : MonoBehaviour
{
    public float speed = 2f;
    public bool IsMoving{get ; private set; }

    public IEnumerator MoveToWaypoints(Transform[] waypoints)
    {
        IsMoving = true;

        foreach (var wp in waypoints)
        {
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
        IsMoving = false;
    }
}
