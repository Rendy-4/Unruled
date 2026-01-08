using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotifInteract : MonoBehaviour
{
   [Header("UI Element")]
   public Image pressFUI;
   [Header("Scene Settings")]
    public Transform teleportTarget;
    public string locationName;

    [Header("Cinemachine Settings")]
    public GameObject MainVcam;
    public GameObject TeleportVcam;

    [Header("Movement Settings")]
    public PlayerMovement3D.MovementMode targetMode;

    private bool playerInRange = false;
    private Transform playerTransform;


    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(TeleportSequence());
        }
    }

    private IEnumerator TeleportSequence()
    {
        if (SceneOverlayUIController.Instance != null)
        {
            SceneOverlayUIController.Instance.PlaySceneText(
                locationName,
                0.5f,
                1.5f
            );
        }

        PlayerMovement3D movement = playerTransform.GetComponent<PlayerMovement3D>();
        if (movement != null) movement.Freeze(true);

        yield return new WaitForSeconds(0.5f);

        if (playerTransform != null && teleportTarget != null)
        {
            playerTransform.position = teleportTarget.position;

            playerTransform.rotation = teleportTarget.rotation;

            if (movement != null)
            {
                float targetFacing = 1f;
                Vector3 newScale = movement.visualsTransform.localScale;
                newScale.x = Mathf.Abs(newScale.x) * targetFacing;
                movement.visualsTransform.localScale = newScale;
            }
        }

        if (MainVcam != null && TeleportVcam != null)
        {
            MainVcam.SetActive(false);
            TeleportVcam.SetActive(true);
        }
        yield return new WaitForSeconds(1.0f);

        if (movement != null) movement.Freeze(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerTransform = other.transform;

            if (pressFUI != null)
            pressFUI.gameObject.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerTransform = null;
            
            if (pressFUI != null)
            pressFUI.gameObject.SetActive(false);
        }
    }

}
