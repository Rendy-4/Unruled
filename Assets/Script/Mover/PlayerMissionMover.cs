using UnityEngine;
using System.Collections;
public class PlayerMissionMover : MissionMover
{
    [Header("Player")]
    public MonoBehaviour playerInput;
    public string forceWalkBool = "forcewalk";
     [Header("Mission")]
    public int requiredMission;
    public bool playOnce = true;
    public Transform[] waypoints;
    private bool hasPlayed;
   

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
        if (IsMoving)
        return;

        if(playOnce && hasPlayed)
        return;

        if(MissionManager.Instance.currentMission != requiredMission)
        return;

        StartCoroutine(MoveRoutine());
    }
    IEnumerator MoveRoutine()
    {

        if(playerInput)
        playerInput.enabled = false;

        if(animator)
        animator.SetBool(forceWalkBool, true);

        yield return StartCoroutine(MoveThroughWaypoints(waypoints));

        if(animator)
        animator.SetBool(forceWalkBool, false);

        if(playerInput) 
        playerInput.enabled = true;

       
    }
}
