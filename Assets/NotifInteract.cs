using UnityEngine;
using UnityEngine.UI;

public class NotifInteract : MonoBehaviour
{
   [Header("UI Element")]
   public Image pressFUI;
   [Header("Scene Settings")]
    public Transform teleportTarget;

    private bool playerInRange = false;
    private Transform playerTransform;


    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if(playerTransform != null && teleportTarget != null)
            playerTransform.position = teleportTarget.position;
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
