using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemDetails ItemDetails;
    private GrabableItem itemCollector;
    private GameManager gameManager;

    void Awake()
    {
        itemCollector = FindAnyObjectByType<InventoryManager>();
        gameManager = FindAnyObjectByType<GameManager>();

        if (itemCollector == null)
        {
            Debug.LogError("InventoryManager tidak ditemukan di scene.", this);
        }

        if (gameManager == null)
        {
            Debug.LogError("GameManager tidak ditemukan di scene.", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && ItemDetails != null && itemCollector != null)
        {
            itemCollector.AddItem(ItemDetails);
            if (CompareTag("Potion") && gameManager != null)
            {
                gameManager.AmbilPotion();
            }

            Destroy(gameObject);
        }
    }
}