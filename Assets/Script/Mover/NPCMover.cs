using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NPCMover : MonoBehaviour
{
    public List<MissionRoute> missionRoutes;

    [Header("Interaction Lock")]
    public MonoBehaviour interactScript;
    public Collider interactCollider;

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
        int mission = MissionManager.Instance.currentMission;

        foreach (var route in missionRoutes)
        {
            if (route.requiredMission != mission)
            continue;
            if (route.PlayOnce && route.used)
            continue;

            StartCoroutine(MoveNpc(route));
            route.used = true;
            break;
        }
        
    }
        IEnumerator MoveNpc(MissionRoute route)
        {
            if(interactScript)
            interactScript.enabled = false;
            if(interactCollider)
            interactCollider.enabled = false;

            yield return StartCoroutine(baseMover.MoveToWaypoints(route.waypoints));

            if(interactScript)
            interactScript.enabled = true;
            if(interactCollider)
            interactCollider.enabled = true;
        }
}
