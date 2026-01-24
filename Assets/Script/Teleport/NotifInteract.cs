using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class NotifInteract : MonoBehaviour
{
   [Header("UI Element")]
   public Image pressFUI;
   [Header("Scene Settings")]
    public Transform teleportTarget;
    public string localtionName;

    [Header("Cinemachine Settings")]
    public GameObject VcamMain;
    public GameObject VcamTarget;
    public GameObject[] AllVcamsToDisable;

    [Header("Movement Settings")]
    public PlayerMovement3D.MovementMode targetMode;

    private bool playerInRange = false;
    private Transform playerTransform;


    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(TeleportSequence());
        }
    }

    private IEnumerator TeleportSequence()
    {
        PlayerMovement3D movement = playerTransform.GetComponent<PlayerMovement3D>();
        if (SceneOverlayUIController.Instance != null)
        {
            SceneOverlayUIController.Instance.PlaySceneText(localtionName, 0.5f, 2.5f);
        }

        if (movement != null)
        {
            movement.Freeze(true);
            movement.currentMode = targetMode;
        }

        yield return new WaitForSeconds(1.0f);

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
        // Handle Cinemachine Vcams
        SwicthCinemachineVcams();
        yield return new WaitForSeconds(1.5f);

        if (movement != null) movement.Freeze(false);
    }

    private void SwicthCinemachineVcams()
    {
        if (VcamMain != null)
        {
            VcamMain.SetActive(false);
        }
        if (AllVcamsToDisable != null)
        {
            foreach (GameObject vcam in AllVcamsToDisable)
            {
                if (vcam != null)
                {
                    vcam.SetActive(false);
                }
            }
        }
        if (VcamTarget != null)
        {
            VcamTarget.SetActive(true);
        }
        else
        {
            if (VcamMain != null)
            {
                VcamMain.SetActive(true);
            }
        }
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
