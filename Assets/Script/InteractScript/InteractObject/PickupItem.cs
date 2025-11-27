using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData data;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        InventoryManager.Instance.AddItem(data);
        Destroy(gameObject);
    }
}
