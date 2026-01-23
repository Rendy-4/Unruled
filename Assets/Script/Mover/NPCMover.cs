using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NPCMover : MonoBehaviour
{
    [Header("Mission Settings")]
    public int requiredMission;
    public bool PlayOnce = true;

    [Header("Waypoints")]
    public Transform[] waypoints;

    [Header("Interaction Lock")]
    public MonoBehaviour interactScript;
    public Collider interactCollider;

    private bool hasPlayed;
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
        if(baseMover.IsMoving)
        return;
        if(PlayOnce && hasPlayed)
        return;

        if(MissionManager.Instance.currentMission != requiredMission)
        return;

        StartCoroutine(MoveNpc());

        IEnumerator MoveNpc()
        {
            hasPlayed = true;

            if(interactScript)
            interactScript.enabled = false;
            if(interactCollider)
            interactCollider.enabled = false;

            yield return StartCoroutine(baseMover.MoveToWaypoints(waypoints));

            if(interactScript)
            interactScript.enabled = true;
            if(interactCollider)
            interactCollider.enabled = true;
        }
    }

}
