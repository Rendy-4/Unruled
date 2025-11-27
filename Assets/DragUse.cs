using UnityEngine;
using UnityEngine.EventSystems;
public class DragUse : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalparent;
    private RectTransform rect;
    private Canvas canvas;
    private CanvasGroup group;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
        canvas = FindAnyObjectByType<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalparent = transform.parent;
        transform.SetParent(canvas.transform);
        group.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        group.blocksRaycasts = true;

        //Raycast ke dunia

        Ray laser = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(laser, out RaycastHit hit))
        {
            InteractableObject obj = hit.collider.GetComponent<InteractableObject>();
            if (obj)
            {
                obj.OnItemUsed(GetComponent<ItemUI>().itemName);

                int index = originalparent.GetSiblingIndex();
                InventoryManager.Instance.RemoveItem(index);
                Destroy(gameObject);
                return;
            }
        }

        //Jika tidak mengenaik object maka kembali ke slot aseli
        transform.SetParent(originalparent);
        rect.anchoredPosition = Vector2.zero;
    }

    
    
}
