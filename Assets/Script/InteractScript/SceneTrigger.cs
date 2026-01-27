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

    [Header("Freeze Player")]
    public bool freezePlayer = true;

    [Header("Teleport (Optional)")]
    public bool useTeleport = true;
    public Transform teleportTarget;

    private bool sudahSelesai;
    private Transform playerTransform;

    void OnTriggerEnter(Collider other)
    {
        if (sudahSelesai) return;
        if (!other.CompareTag("Player")) return;

        if (!MissionManager.Instance.ValidateMission(missionOrder))
            return;

        playerTransform = other.transform;
        StartCoroutine(HandleScene());
    }

    IEnumerator HandleScene()
    {
        sudahSelesai = true;

        PlayerMovement3D controller =
            playerTransform.GetComponent<PlayerMovement3D>();

        if (freezePlayer && controller != null)
            controller.Freeze(true);

        if (!string.IsNullOrEmpty(sceneText) &&
            SceneOverlayUIController.Instance != null)
        {
            SceneOverlayUIController.Instance.PlaySceneText(
                sceneText,
                fadeDuration,
                displayTime
            );
        }

        yield return new WaitForSeconds(fadeDuration);

        if (useTeleport && teleportTarget != null)
        {
            playerTransform.position = teleportTarget.position;
            playerTransform.rotation = teleportTarget.rotation;
        }

        yield return new WaitForSeconds(displayTime);

        if (freezePlayer && controller != null)
            controller.Freeze(false);

        GetComponent<Collider>().enabled = false;
    }
}
