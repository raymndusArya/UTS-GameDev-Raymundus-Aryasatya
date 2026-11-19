using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float jarakSerang = 1.5f;
    [SerializeField] private int damageSerang = 25;

    void Update()
    {
        // Klik kiri mouse -> serang
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Klik terdeteksi, mencoba menyerang...");
            Serang();
        }
    }

    void Serang()
    {
        // Cari semua collider dalam radius jarakSerang di sekitar player
        Collider2D[] hasil = Physics2D.OverlapCircleAll(transform.position, jarakSerang);

        Debug.Log("Jumlah collider dalam radius: " + hasil.Length);

        foreach (Collider2D col in hasil)
        {
            // Cek apakah objek yang kena punya komponen Enemy
            // (otomatis berlaku juga untuk ZombieBiasa & ZombieCepat, karena keduanya turunan Enemy)
            Enemy zombie = col.GetComponent<Enemy>();
            if (zombie != null)
            {
                zombie.KenaDamage(damageSerang);
                Debug.Log("Player menyerang " + zombie.name);
            }
        }
    }

    // Biar radius serang kelihatan di Scene view saat GameObject player dipilih
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, jarakSerang);
    }
}