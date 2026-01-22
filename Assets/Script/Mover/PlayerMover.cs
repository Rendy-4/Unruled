using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerMover : MonoBehaviour
{   
    public List<MissionRoute> missionRoutes;

    [Header("Player Control")]
    public MonoBehaviour playerInput;   
    private MoverBase basemover;

    [Header("Animation")]
    public Animator animator;
    public string forceWalkBool = "forceWalk";

    void Awake()
    {
        basemover = GetComponent<MoverBase>();
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
        if (basemover.IsMoving)
        return;

        int mission = MissionManager.Instance.currentMission;

        foreach (var route in missionRoutes)
        {
            if (route.requiredMission != mission)
                continue;

            if (route.PlayOnce && route.used)
                continue;

            StartCoroutine(MovePlayer(route));
            route.used = true;
            break;
        }
    }

    IEnumerator MovePlayer(MissionRoute route)
    {
        if (playerInput)
            playerInput.enabled = false;

        if (animator)
        animator.SetBool(forceWalkBool, true);

            yield return StartCoroutine(basemover.MoveToWaypoints(route.waypoints));

        if (animator)
        animator.SetBool(forceWalkBool, false);

        if(playerInput)
        playerInput.enabled = true;
    }

    public void StartMove(Transform[] waypoints)
    {
        if (basemover.IsMoving)
        return;

        StartCoroutine(MoveByTrigger(waypoints));
    }

    IEnumerator MoveByTrigger(Transform[] waypoints)
    {
        if(playerInput)
        playerInput.enabled = false;

        if (animator)
        animator.SetBool(forceWalkBool, true);

        yield return StartCoroutine(basemover.MoveToWaypoints(waypoints));

        if (animator)
        animator.SetBool(forceWalkBool, false);

        if(playerInput)
        playerInput.enabled = true;
    }
}
