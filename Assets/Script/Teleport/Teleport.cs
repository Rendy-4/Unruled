using GLTFast.Schema;
using UnityEngine;

public class SceneTeleportTrigger : MonoBehaviour
{
     [Header("Teleport")]
    public Transform teleportTarget;

    [Header("Scene Text")]
    [TextArea(2, 4)]
    public string sceneText;

    [Header("Display Settings")]
    public float fadeDuration = 1f;
    public float displayTime = 3f;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (!other.CompareTag("Player"))return;

            if(teleportTarget == null)
            {
                Debug.LogError("TELEPORT TARGET BELUM DI ISI ", this);
                return;
            }
            
            if (!string.IsNullOrEmpty(sceneText) && SceneOverlayUIController.Instance != null)
            {
                SceneOverlayUIController.Instance.PlaySceneText(sceneText,fadeDuration,displayTime);
            }
             other.transform.position = teleportTarget.position;
             
        }
    }
}
