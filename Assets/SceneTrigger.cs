using UnityEngine;

public class SceneMissionTrigger : MonoBehaviour
{
    [Header("Mission")]
    public int missionOrder;

    [Header("Scene Text")]
    [TextArea(3, 6)]
    public string sceneText;

    private bool sudahSelesai;

    private void OnTriggerEnter(Collider other)
    {
        if (sudahSelesai) return;
        if (!other.CompareTag("Player")) return;

        // Validasi mission
        bool valid = MissionManager.Instance.ValidateMission(missionOrder);
        if (!valid) return;

        // Mainkan scene
        SceneOverlayUIController.Instance.PlaySceneText(sceneText);

        // Tandai selesai
        sudahSelesai = true;

        // Optional: disable trigger
        GetComponent<Collider>().enabled = false;
    }
}
