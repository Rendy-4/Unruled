using UnityEngine;
using System.Collections;

public class BaseMoverTest : MonoBehaviour
{
    public Transform[] waypoints;

    void Start()
    {
        MoverBase mover = GetComponent<MoverBase>();
        StartCoroutine(mover.MoveToWaypoints(waypoints));
    }
}
