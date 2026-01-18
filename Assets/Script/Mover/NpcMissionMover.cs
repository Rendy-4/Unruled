using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

public class NpcMissionMover : MonoBehaviour
{
    [Header("Movement Routes")]
    public List<MissionMoveRoute> routes = new List<MissionMoveRoute>();

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public bool playOncePerMission = true;

    private bool isMoving;
    private HashSet<int> playedMissions = new HashSet<int>();

    private void OnEnable()
    {
        MissionManager.OnMissionUpdated += CheckMission;
    }
    private void OnDisable()
    {
        MissionManager.OnMissionUpdated -= CheckMission;
    }

    private void Start()
    {
        CheckMission();
    }

    private void CheckMission()
    {
        if (isMoving) 
        return;

        int currentMission = MissionManager.Instance.currentMission;
        foreach (var route in routes)
        {
            if (route.requiredMission != currentMission)
            continue;

            if(playOncePerMission && playedMissions.Contains(route.requiredMission))
            return;

            StartCoroutine(MoveRoutine(route));
            break;
        }
    }

    private IEnumerator MoveRoutine(MissionMoveRoute route)
    {
        isMoving = true;
        playedMissions.Add(route.requiredMission);
        foreach (Transform target in route.waypoints)
        {
            if (target == null)
            continue;

            while (Vector3.Distance(transform.position, target.position) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target.position,
                    moveSpeed * Time.deltaTime
                );
                yield return null;
            }
        }
        isMoving = false;
    }
}
[System.Serializable]
    public class MissionMoveRoute
    {
        public int requiredMission;
        public Transform[] waypoints;
    }