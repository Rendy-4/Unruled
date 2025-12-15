using UnityEngine;
using System.Collections;

public class SceneMissionTrigger : MonoBehaviour
{
    [Header("Mission")]
    public int missionOrder;

    [Header("Scene Text")]
    [TextArea(3, 6)]
    public string sceneText;

    [Header("Display Settings")]
    public float fadeDuration = 0.5f;
    public float displayTime = 1.5f;

    [Header("Freeze Player (Optional)")]
    public bool freezePlayer = true;
    public float freezeDuration = 0.5f;

    [Header("Teleport (Optional)")]
    public bool useTeleport = false;
    public Transform teleportTarget;

    private bool sudahSelesai;

    private void OnTriggerEnter(Collider other)
    {
        if (sudahSelesai) return;
        if (!other.CompareTag("Player")) return;

        bool valid = MissionManager.Instance.ValidateMission(missionOrder);
        if (!valid) return;

        StartCoroutine(HandleScene(other));
    }

    private IEnumerator HandleScene(Collider player)
    {
        sudahSelesai = true;

        PlayerMovement3D controller = player.GetComponent<PlayerMovement3D>();

        // ▶ Freeze player
        if (freezePlayer && controller != null)
            controller.Freeze(true);

        // ▶ Scene overlay
        if (!string.IsNullOrEmpty(sceneText) &&
            SceneOverlayUIController.Instance != null)
        {
            SceneOverlayUIController.Instance.PlaySceneText(
                sceneText,
                fadeDuration,
                displayTime
            );
        }

        // ⏳ Tunggu fade in
        yield return new WaitForSeconds(fadeDuration);

        // ▶ Teleport (optional)
        if (useTeleport && teleportTarget != null)
        {
            player.transform.position = teleportTarget.position;
        }

        // ⏳ Tunggu display
        yield return new WaitForSeconds(displayTime);

        // ▶ Unfreeze
        if (freezePlayer && controller != null)
            controller.Freeze(false);

        // ▶ Matikan trigger
        GetComponent<Collider>().enabled = false;
    }
}
