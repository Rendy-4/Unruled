using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MissionMoveData
{
    public int requiredMission;
    public bool playOnce = true;
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

        yield return StartCoroutine(baseMover.MoveToWaypoints(move.waypoints));

        UnlockInteraction();
        isRunning = false;
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
}
