using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleportTrigger : MonoBehaviour
{
    [Header("Scene Settings")]
    public Transform teleportTarget; // posisi tujuan teleport

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             other.transform.position = teleportTarget.position;
        }
    }
}
