using UnityEngine;

public class Camerachanger : MonoBehaviour
{
    public Transform cameraTransform;      // referensi ke kamera
    public Vector3 targetRotation;         // rotasi baru yang kamu inginkan (Euler)
    public float rotateSpeed = 2f;

    private bool rotating = false;

    private void Update()
    {
        if (rotating)
        {
            cameraTransform.rotation = Quaternion.Lerp(
                cameraTransform.rotation,
                Quaternion.Euler(targetRotation),
                Time.deltaTime * rotateSpeed
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rotating = true;
        }
    }
}
