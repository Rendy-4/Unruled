using UnityEngine;

public class NpcMissionController : MonoBehaviour
{
    [System.Serializable]
    public class MissionState
    {
        public int missionIndex;
        public Transform targetPosition;
        public bool visible = true;

    }

    public MissionState[] missionStates;

    private Collider npcCollider;
    private Renderer[] renderers;

    void Awake()
    {
        npcCollider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    void OnEnable()
    {
        MissionManager.OnMissionUpdated += Updatestate;
    }

    void OnDisable()
    {
        MissionManager.OnMissionUpdated -= Updatestate;
    }

    void Start()
    {
        Updatestate();
    }

    void Updatestate()
    {
        int mission = MissionManager.Instance.currentMission;

        foreach (var state in missionStates)
        {
            
            if (state.missionIndex == mission)
            {
                // Posisi Npc
                if (state.targetPosition != null)
                {
                    transform.SetPositionAndRotation(
                        state.targetPosition.position,
                        state.targetPosition.rotation
                    );
                }

                // Visibilitas
                foreach (var r in renderers)
                {
                    r.enabled = state.visible;
                }

                if(npcCollider != null)
                    npcCollider.enabled = state.visible;
                    return;  
            }
        }
    }
}
