using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item Data
    public string itemName;
    public int itemQuantity;
    public Sprite itemIcon;
    public bool isFull;

    // Item Slot
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private int maxStackSize = 99;
    // Selected Item
    [SerializeField] public GameObject selectedPanel;
    public bool isItemSelected;
    public InventoryManager inventoryManager;
    public bool IsEmpty => itemQuantity <= 0;

    private void Awake()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        RefreshUI();
    }

    public int AddItem(ItemDetails item, int amount)
    {
        if (item == null || !IsEmpty || amount <= 0)
        {
            return amount;
        }

        itemName = item.ItemName;
        itemIcon = item.ItemIcon;
        itemQuantity = 0;
        isFull = false;
        return AddQuantity(amount);
    }

    public bool CanStackWith(string otherItemName)
    {
        return !IsEmpty && !isFull && itemName == otherItemName;
    }

    public int AddQuantity(int amount)
    {
        int space = Mathf.Max(0, maxStackSize - itemQuantity);
        int added = Mathf.Min(amount, space);
        int leftover = amount - added;

        itemQuantity += added;
        isFull = itemQuantity >= maxStackSize;
        RefreshUI();

        return leftover;
    }

    private void RefreshUI()
    {
        bool hasItem = !IsEmpty;

        if (itemImage != null)
        {
            itemImage.sprite = hasItem ? itemIcon : null;
            itemImage.enabled = hasItem && itemIcon != null;
            itemImage.preserveAspect = true;
        }

        if (quantityText != null)
        {
            quantityText.text = hasItem ? itemQuantity.ToString() : string.Empty;
            quantityText.enabled = hasItem;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick();
        }
    }

    private void LeftClick()
    {
        Debug.Log("Left Clicked on " + itemName);
        inventoryManager.DesselectAllSlot();
        selectedPanel.SetActive(true);
        isItemSelected = true;
    }

    private void RightClick()
    {
        Debug.Log("Right Clicked on " + itemName);
    }
}
