using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int skor = 0;
    public float kecepatan = 5f;
    private Vector2 arahGerak;
    public GameManager gameManager;
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
        // TODO: cek apakah yang disentuh punya tag "Coin"
        if (other.CompareTag("Koin"))
        {
            // TODO: hancurkan koin yang tersentuh
            Destroy(other.gameObject);
            // TODO: tambah skor sebanyak 1
            skor++;
            // TODO: tampilkan skor ke Console
            Debug.Log("Total Skor : " + skor);
            gameManager.AmbilKoin();
        }
    }
}