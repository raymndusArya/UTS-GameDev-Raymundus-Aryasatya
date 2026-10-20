using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemDetails ItemDetails;
    private GrabableItem itemCollector;

    void Awake()
    {
        itemCollector = FindAnyObjectByType<InventoryManager>();

        if (itemCollector == null)
        {
            Debug.LogError("InventoryManager tidak ditemukan di scene.", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && ItemDetails != null && itemCollector != null)
        {
            itemCollector.AddItem(ItemDetails);
            Destroy(gameObject);
        }
    }
}