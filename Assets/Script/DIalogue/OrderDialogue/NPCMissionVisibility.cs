using UnityEngine;

public class NPCMissionVisibility : MonoBehaviour
{
   public int appearAtMission = 0;
   public int dissapearAfter = -1;
   private Collider npcCollider;
   private Renderer[] renderers;

    void Awake()
    {
        npcCollider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    void OnEnable()
    {
        MissionManager.OnMissionUpdated += UpdateVisibility;
    }
    void OnDisable()
    {
        MissionManager.OnMissionUpdated -= UpdateVisibility;
    }

    void Start()
    {
        UpdateVisibility();
    }

    void UpdateVisibility()
    {
       int mission = MissionManager.Instance.currentMission;

       bool ShouldAppear = mission >= appearAtMission && (dissapearAfter < 0 || mission <= dissapearAfter);
       gameObject.SetActive(ShouldAppear);
    }
}
