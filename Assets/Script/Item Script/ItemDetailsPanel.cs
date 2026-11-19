using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailsPanel : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text textItemName;
    [SerializeField] private TMP_Text textQuantity;
    [SerializeField] private TMP_Text textDescription;

    private void Awake()
    {
        Clear();
    }

    public void Show(ItemDetails item, int quantity)
    {
        if (item == null)
        {
            Clear();
            return;
        }

        itemImage.sprite = item.ItemIcon;
        itemImage.enabled = item.ItemIcon != null;
        textItemName.text = item.ItemName;
        textQuantity.text = quantity.ToString();
        textDescription.text = item.Description;
    }

    public void Clear()
    {
        itemImage.sprite = null;
        itemImage.enabled = false;
        textItemName.text = "";
        textQuantity.text = "";
        textDescription.text = "";
    }
}