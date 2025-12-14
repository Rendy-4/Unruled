using UnityEngine;

public class SceneMissionTrigger : MonoBehaviour
{
    [Header("Mission")]
    public int missionOrder;

    [Header("Scene Text")]
    [TextArea(3, 6)]
    public string sceneText;
    private bool sudahSelesai;
     [Header("Display Settings")]
    public float fadeDuration = 0.1f;
    public float displayTime = 0.1f;

    private void OnTriggerEnter(Collider other)
    {
        if (sudahSelesai) return;
        if (!other.CompareTag("Player")) return;

        // Validasi mission
        bool valid = MissionManager.Instance.ValidateMission(missionOrder);
        if (!valid) return;

        // Mainkan scene
        SceneOverlayUIController.Instance.PlaySceneText(sceneText,fadeDuration,displayTime);

        // Tandai selesai
        sudahSelesai = true;

        // Optional: disable trigger
        GetComponent<Collider>().enabled = false;
    }
}
