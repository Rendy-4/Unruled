using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string requiredItem;
    public bool OnItemUsed(string itemname)
    {
        if (itemname == requiredItem)
        {
            Debug.Log("Item Cocok! oobjek diaktifkan.");
            return true;
        }
        else
        {
            Debug.Log("Item tidak cocok!.");
            return false;
        }
    }
}
