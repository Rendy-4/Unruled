using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[System.Serializable]
public class MissionMoveData
{
    public int requiredMission;
    public bool playOnce = true;
    public bool isTeleport = false;
    public Transform[] waypoints;
}
public class NPCMover : MonoBehaviour
{
    [Header("Mission Moves")]
    public List<MissionMoveData> missionMoves;

    [Header("Interaction Lock")]
    public MonoBehaviour interactScript;
    public Collider interactCollider;

    private HashSet<int> playedMissions = new HashSet<int>();
    private bool isRunning;
    private MoverBase baseMover;

    void Awake()
    {
        baseMover = GetComponent<MoverBase>();
    }

    void OnEnable()
    {
        MissionManager.OnMissionUpdated += CheckMission;
    }

    void OnDisable()
    {
        MissionManager.OnMissionUpdated -= CheckMission;
    }

    void CheckMission()
    {
        if (isRunning || baseMover.IsMoving)
            return;

        foreach (var move in missionMoves)
        {
            if (move.requiredMission != MissionManager.Instance.currentMission)
                continue;

            if (move.playOnce && playedMissions.Contains(move.requiredMission))
                continue;

            StartCoroutine(RunMove(move));
            break;
        }
    }

    IEnumerator RunMove(MissionMoveData move)
    {
        isRunning = true;
        playedMissions.Add(move.requiredMission);

        LockInteraction();
        if (move.isTeleport)
        {
            if(move.waypoints != null && move.waypoints.Length > 0)
            {
                transform.SetPositionAndRotation(
                move.waypoints[move.waypoints.Length - 1].position,
                move.waypoints[move.waypoints.Length - 1].rotation
            );
            }
        }
        else
        {
         yield return StartCoroutine(baseMover.MoveToWaypoints(move.waypoints));   
        }

        

        UnlockInteraction();
        isRunning = false;
        var missionController = GetComponent<NpcMissionController>();
        if (missionController != null)
            missionController.Updatestate();
    }

    void LockInteraction()
    {
        if (interactScript) interactScript.enabled = false;
        if (interactCollider) interactCollider.enabled = false;
    }

    void UnlockInteraction()
    {
        if (interactScript) interactScript.enabled = true;
        if (interactCollider) interactCollider.enabled = true;
    }
    public int GetLastPlayedMission()
    {
        int max = 0;
        foreach (int m in playedMissions)
            if (m > max) max = m;
        return max;
    }
     public void MarkMissionPlayed(int mission)
    {
        playedMissions.Add(mission);
    }

    public bool HasPlayedMissions()
    {
        return playedMissions.Count > 0;
    }

}
