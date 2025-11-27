using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemname;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        InventoryManager.Instance.AddItem(itemname);
        Destroy(gameObject);
    }
}
