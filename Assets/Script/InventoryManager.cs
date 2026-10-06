using UnityEngine;
using UnityEngine.InputSystem;
public class InventoryManager : MonoBehaviour
{

    InputAction inventoryAction;
    [SerializeField] GameObject inventoryCanvas;
    private bool InventoryActive;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryAction = InputSystem.actions.FindAction("Inventory"); 
        inventoryCanvas.SetActive(false);
        InventoryActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inventoryAction.triggered && InventoryActive == false)
        {
            inventoryCanvas.SetActive(true);
            InventoryActive = true;
            Time.timeScale = 0f;
            Debug.Log("Inventory Muncul");
        } else if(inventoryAction.triggered && InventoryActive == true)
        {
            inventoryCanvas.SetActive(false);
            InventoryActive = false;
            Time.timeScale = 1f;
            Debug.Log("Inventory Hilang");
        }
    }
}
