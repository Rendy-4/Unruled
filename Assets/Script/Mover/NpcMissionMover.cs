using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NpcMissionMover : MonoBehaviour
{
    [Header("Animation")]
    public Animator animator;
    public string walkBoolName = "isWalk";

    [Header("Interaction")]
    public MonoBehaviour interactScript;
    public Collider interactCollider;

    [Header("Movement Routes")]
    public List<MissionMoveRoute> routes = new List<MissionMoveRoute>();

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public bool playOncePerMission = true;
    private bool isMoving;
    private HashSet<int> playedMissions = new HashSet<int>();

    private float LockedY;
    private bool LockYActive;

    public bool IsMoving()
    {
        return isMoving;
    }
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
    private IEnumerator MoveRoutine(MissionMoveRoute route)
    {
        isMoving = true;
        playedMissions.Add(route.requiredMission);

        SetInteract(false);
        SetWalking(true);

        LockedY = transform.position.y;
        LockYActive = true;

        foreach (Transform target in route.waypoints)
        {
            if (target == null)
            continue;

            while (true)
            {
                Vector3 targetposition = target.position;
                targetposition.y = transform.position.y;

            if (Vector3.Distance(transform.position, targetposition) <= 0.05f)
                break;

                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetposition,
                    moveSpeed * Time.deltaTime
                );

                yield return null;
            }   
        }
        SetWalking(false);
        SetInteract(true);

        LockYActive = false;
        isMoving = false;
    }
    private void LateUpdate()
    {
        if (!LockYActive)
        return;

        Vector3 pos = transform.position;
        pos.y = LockedY;
        transform.position = pos;
    }
    
}
    

[System.Serializable]
    public class MissionMoveRoute
    {
        public int requiredMission;
        public Transform[] waypoints;
    }