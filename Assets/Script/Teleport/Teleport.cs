using UnityEngine;
using System.Collections;

public class SceneTeleportTrigger : MonoBehaviour
{
    [Header("Teleport")]
    public Transform teleportTarget;

    [Header("Scene Text")]
    [TextArea(3, 6)]
    public string sceneText;

    [Header("Display Settings")]
    public float fadeDuration = 0.5f;
    public float displayTime = 1.5f;

    [Header("Player Freeze")]
    public float freezeDuration = 0.5f;
    private bool isProcessing;
    public float cooldown = 1f;


   private void OnTriggerEnter(Collider other)
    {
    if (isProcessing) return;
    if (!other.CompareTag("Player")) return;

    StartCoroutine(HandlePortal(other));
    }


    private IEnumerator HandlePortal(Collider player)
{
    isProcessing = true;

    PlayerMovement3D controller = player.GetComponent<PlayerMovement3D>();
    if (controller != null)
        controller.Freeze(true);

        //Fade
        if (!string.IsNullOrEmpty(sceneText) && SceneOverlayUIController.Instance != null)
        {
            SceneOverlayUIController.Instance.PlaySceneText(
                sceneText,
                fadeDuration,
                displayTime
            );
        }

    yield return new WaitForSeconds(fadeDuration);

    player.transform.position = teleportTarget.position;

    yield return new WaitForSeconds(displayTime);

    if (controller != null)
        controller.Freeze(false);

    yield return new WaitForSeconds(cooldown);
    isProcessing = false;
}

}
