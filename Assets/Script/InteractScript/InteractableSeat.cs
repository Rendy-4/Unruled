using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractableSeat : MonoBehaviour
{
    [Header("UI Elements")]
    public Image pressFUI;

    [Header("Seat Settings")]
    public Transform seatPosition;
    public Transform standPosition;

    private string sittingBool = "IsSitting";

    private bool playerInRange = false;
    private bool isSitting = false;
    private GameObject player;
    private PlayerMovement3D playerMovent;

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!isSitting)
            {
                SitDown();
            }
            else
            {
                StandUp();
            }
        }
    }

    private void SitDown()
    {
        isSitting = true;
        
        if (playerMovent != null)
        {
            playerMovent.Freeze(true);
            
            Collider playerCollider = player.GetComponent<Collider>();
            if (playerCollider != null)
            {
                playerCollider.enabled = false;
            }

            player.transform.position = seatPosition.position;
            player.transform.rotation = seatPosition.rotation;

            if (playerCollider != null)
                playerCollider.enabled = true;

            if (playerMovent.anim != null)
                playerMovent.anim.SetBool(sittingBool, true);

            if (pressFUI != null)
                pressFUI.gameObject.SetActive(false);
        }
    }

    private void StandUp()
    {
        isSitting = false;

        if (playerMovent != null)
        {
            if (playerMovent.anim != null)
                playerMovent.anim.SetBool(sittingBool, false);
            
            if (standPosition != null)
            {
                player.transform.position = standPosition.position;
                player.transform.rotation = standPosition.rotation;
            }

            playerMovent.Freeze(false);
            if (pressFUI != null && playerInRange)
                pressFUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
            playerMovent = player.GetComponent<PlayerMovement3D>();

            if (!isSitting && pressFUI != null)
                pressFUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (!isSitting && pressFUI != null)
                pressFUI.gameObject.SetActive(false);
        }
    }
}
