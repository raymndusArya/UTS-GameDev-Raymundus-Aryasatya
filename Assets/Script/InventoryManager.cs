using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, GrabableItem
{
    InputAction inventoryAction;
    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private int slotCount = 16;
    private bool InventoryActive;
    public ItemSlot[] itemSlot;

    void Start()
    {
        CreateSlots();
        InventoryCanvas.SetActive(false);
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        InventoryActive = false;
    }

    void Update()
    {
        if (inventoryAction == null || InventoryCanvas == null) return;

        if (inventoryAction.triggered)
        {
            InventoryActive = !InventoryActive;
            Time.timeScale = InventoryActive ? 0f : 1f;
            InventoryCanvas.SetActive(InventoryActive);
            Debug.Log(InventoryActive ? "Inventory is active" : "Inventory is deactivated");
        }
    }

    // Build the slot objects at runtime so no manual wiring is needed in the scene
    private void CreateSlots()
    {
        ItemSlot slotPrefab = Resources.Load<ItemSlot>("ItemSlot");
        if (slotPrefab == null)
        {
            Debug.LogError("ItemSlot prefab was not found in a Resources folder.", this);
            itemSlot = new ItemSlot[0];
            return;
        }

        Transform parent = InventoryCanvas.transform;
        GridLayoutGroup grid = InventoryCanvas.GetComponentInChildren<GridLayoutGroup>(true);
        if (grid != null)
        {
            parent = grid.transform;

            for (int i = grid.transform.childCount - 1; i >= 0; i--)
            {
                GameObject placeholder = grid.transform.GetChild(i).gameObject;
                placeholder.SetActive(false);
                Destroy(placeholder);
            }
        }

        itemSlot = new ItemSlot[slotCount];
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i] = Instantiate(slotPrefab, parent);
        }
    }

    public void AddItem(ItemDetails item)
    {
        if (item == null)
        {
            Debug.LogError("Cannot add a null item to the inventory.", this);
            return;
        }

        Debug.Log("Item added to inventory: " + item.ItemName + " Quantity: " + item.Quantity);

        if (itemSlot == null || itemSlot.Length == 0)
        {
            Debug.LogWarning("Inventory has no slots, '" + item.ItemName + "' was lost.");
            return;
        }

        int remaining = item.Quantity;
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i] != null && itemSlot[i].IsEmpty && itemSlot[i].AddItem(item))
            {
                remaining--;
                if (remaining <= 0) return;
            }
        }

        Debug.LogWarning("Inventory is full, " + remaining + "x '" + item.ItemName + "' was lost.");
    }

    // Total jumlah satu jenis item di semua slot (berguna untuk HUD koin)
    public int GetTotal(string itemName)
    {
        int total = 0;
        foreach (ItemSlot slot in itemSlot)
        {
            if (!slot.IsEmpty && slot.itemName == itemName)
                total += slot.itemQuantity;
        }
        return total;
    }

    public void DesselectAllSlot()
    {
        foreach (ItemSlot slot in itemSlot)
        {
            slot.isItemSelected = false;
            slot.selectedPanel.SetActive(false);
        }
    }
}