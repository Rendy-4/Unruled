using UnityEngine;

public class NPCRotate : MonoBehaviour
{
    [Header("Model Pivot (bukan animator)")]
    public Transform modelPivot;

    [Header("Rotation")]
    public float enterY = -90f;
    public float exitY = 0f;
    [Header("Optional")]
    public bool useLocalRotation = true;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SetYRotation(enterY);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SetYRotation(exitY);
    }

    void SetYRotation(float y)
    {
        if (modelPivot == null)
        {
            Debug.LogWarning("ModelPivot belum di-assign", this);
            return;
        }

        if (useLocalRotation)
        {
            Vector3 rot = modelPivot.localEulerAngles;
            rot.y = y;
            modelPivot.localEulerAngles = rot;
        }
        else
        {
            Vector3 rot = modelPivot.eulerAngles;
            rot.y = y;
            modelPivot.eulerAngles = rot;
        }
        
    }
}
