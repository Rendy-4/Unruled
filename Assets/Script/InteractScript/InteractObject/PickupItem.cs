using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData data;
    public string UniqueID;


    void Start()
    {
        if (InventoryManager.Instance.collectedItemIDs.Contains(data.itemID))
        {
            Destroy(gameObject);
            return;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        InventoryManager.Instance.AddItem(data);

        DataPresistenceManager.instance.SaveGame();
        Destroy(gameObject);
    }
}
