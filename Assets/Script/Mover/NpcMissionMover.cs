using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NpcMissionMover : MissionMover
{
    [Header("Animation")]
    public string walkBoolName = "isWalk";

    [Header("Interaction")]
    public MonoBehaviour interactScript;
    public Collider interactCollider;

    [Header("Movement Routes")]
    public List<MissionMoveRoute> routes = new List<MissionMoveRoute>();
    public bool playOncePerMission = true;
    private HashSet<int> playedMissions = new HashSet<int>();
    private void OnEnable()
    {
        MissionManager.OnMissionUpdated += CheckMission;
    }
    private void OnDisable()
    {
        MissionManager.OnMissionUpdated -= CheckMission;
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

            StartCoroutine(Move(route));
            break;
        }
    }

    private void SetWalking(bool value)
    {
        if (animator == null)
        return;
        animator.SetBool(walkBoolName, value);
    }
    private void SetInteract(bool value)
    {
        if(interactScript != null)
        interactScript.enabled = value;

        if(interactCollider != null)
        interactCollider.enabled = value;
    }
    private IEnumerator Move(MissionMoveRoute route)
    {
        playedMissions.Add(route.requiredMission);

        SetInteract(false);
        SetWalking(true);

        yield return StartCoroutine(MoveThroughWaypoints(route.waypoints));

        SetWalking(false);
        SetInteract(true);  
    }
   
}
    

[System.Serializable]
    public class MissionMoveRoute
    {
        public int requiredMission;
        public Transform[] waypoints;
    }