using UnityEngine;
using UnityEngine.UI;

public class NotifF : MonoBehaviour
{
    [Header("UI")]
    public Image pressFUI;

    [Header("Mission Condition")]
    public int requiredMission = 0;

    private bool playerInRange;

    void Start()
    {
        if (pressFUI != null)
            pressFUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!playerInRange) return;
        if (!CanInteract()) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F ditekan → siap dialog / monolog");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!CanInteract()) return;

        playerInRange = true;

        if (pressFUI != null)
            pressFUI.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (pressFUI != null)
            pressFUI.gameObject.SetActive(false);
    }

    bool CanInteract()
    {
        if (MissionManager.Instance == null) return false;

        return MissionManager.Instance.currentMission >= requiredMission;
    }
}
