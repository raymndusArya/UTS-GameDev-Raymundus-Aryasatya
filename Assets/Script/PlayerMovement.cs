using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int skor = 0;
    public float kecepatan = 5f;
    private Vector2 arahGerak;
    public GameManager gameManager;
    [SerializeField] private ItemDetails coinDetails;

    private InventoryManager inventoryManager;

    void Awake()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();

        if (coinDetails == null)
        {
            coinDetails = Resources.Load<ItemDetails>("Koin");
        }
    }

    void OnMove(InputValue value)
    {
        arahGerak = value.Get<Vector2>();
    }

    void Update()
    {
        Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0);
        transform.position += arah * kecepatan * Time.deltaTime;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Koin"))
        {
            if (coinDetails == null)
            {
                Debug.LogError("Coin ItemDetails belum diatur pada PlayerMovement.", this);
                return;
            }

            if (inventoryManager == null)
            {
                Debug.LogError("InventoryManager tidak ditemukan di scene.", this);
                return;
            }

            inventoryManager.AddItem(coinDetails);
            Destroy(other.gameObject);
            skor++;
            Debug.Log("Total Skor : " + skor);
            gameManager.AmbilKoin();
        }
    }
}