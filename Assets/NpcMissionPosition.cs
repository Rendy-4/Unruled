using UnityEngine;

public class NPCMissionPosition : MonoBehaviour
{
    [System.Serializable]
    public class MissionPosition
    {
        public int missionIndex;          // MissionOrder
        public Transform targetPosition;  // Posisi NPC
    }

    [Header("Mission Position Settings")]
    public MissionPosition[] positions;

    [Header("Disappear After Mission (-1 = never)")]
    public int disappearAfter = -1;

    private void OnEnable()
    {
        MissionManager.OnMissionUpdated += UpdatePosition;
    }

    private void OnDisable()
    {
        MissionManager.OnMissionUpdated -= UpdatePosition;
    }

    private void Start()
    {
        UpdatePosition(); // update saat load game
    }

    void UpdatePosition()
    {
        int currentMission = MissionManager.Instance.currentMission;

        // 🔴 Hilang jika lewat batas
        if (disappearAfter >= 0 && currentMission > disappearAfter)
        {
            gameObject.SetActive(false);
            return;
        }

        // 🔵 Cari posisi sesuai mission
        foreach (var pos in positions)
        {
            if (pos.missionIndex == currentMission && pos.targetPosition != null)
            {
                transform.position = pos.targetPosition.position;
                transform.rotation = pos.targetPosition.rotation;
                gameObject.SetActive(true);
                return;
            }
        }

        // ❌ Tidak punya posisi untuk mission ini
        gameObject.SetActive(false);
    }
}
