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

    [Header("Player Settings")]
    public float cooldown = 1f;
    public PlayerMovement3D.MovementMode targetMode;

    [Header("Camera Settings")]
    public GameObject VcamMain;
    public GameObject VcamTarget;

    private bool isProcessing;
    


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
        controller.currentMode = targetMode;

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
    player.transform.rotation = teleportTarget.rotation;

    yield return new WaitForSeconds(displayTime);

    if (controller != null && controller.visualsTransform != null)
    {
        Vector3 newScale = controller.visualsTransform.localScale;
        newScale.x = Mathf.Abs(newScale.x);
        controller.visualsTransform.localScale = newScale;
    }

    if (VcamMain != null) VcamMain.SetActive(true);
    if (VcamTarget != null) VcamTarget.SetActive(false);

    yield return new WaitForSeconds(cooldown);
    isProcessing = false;

    if (controller != null)
        controller.Freeze(false);
}

}
