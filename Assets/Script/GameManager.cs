using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalKoin;
    private int koinTerkumpul = 0;
    private int jumlahZombieMati = 0;

    private bool gameOver = false;
    private bool menang = false;

    void Start()
    {
        totalKoin = GameObject.FindGameObjectsWithTag("Koin").Length;

        // Cari player, lalu subscribe (mendengarkan) event OnPlayerMati
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnPlayerMati += SaatPlayerMati;
        }

        // Cari SEMUA zombie yang ada di scene saat game mulai,
        // lalu subscribe ke event OnZombieMati masing-masing
        Enemy[] semuaZombie = FindObjectsOfType<Enemy>();
        foreach (Enemy zombie in semuaZombie)
        {
            zombie.OnZombieMati += SaatZombieMati;
        }
    }

    // Penerima Event: dipanggil otomatis saat PlayerHealth.OnPlayerMati di-Invoke
    void SaatPlayerMati()
    {
        gameOver = true;
        Debug.Log("GameManager dengar event. Player mati -> GAME OVER");
    }

    // Penerima Event: dipanggil otomatis saat Enemy.OnZombieMati di-Invoke
    void SaatZombieMati(Enemy zombie)
    {
        jumlahZombieMati++;
        Debug.Log("GameManager dengar event. Zombie mati: " + jumlahZombieMati + " (" + zombie.name + ")");
    }

    public void AmbilKoin()
    {
        if (gameOver) return;

        koinTerkumpul++;
        if (koinTerkumpul == totalKoin)
        {
            Menang();
        }
    }

    void Menang()
    {
        menang = true;
        Debug.Log("KAMU MENANG!");
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 22;
        GUI.Label(new Rect(16, 16, 480, 36), "Koin: " + koinTerkumpul + " / " + totalKoin);
        GUI.Label(new Rect(16, 52, 480, 36), "Zombie mati: " + jumlahZombieMati);

        if (gameOver)
        {
            GUI.Label(new Rect(16, 88, 480, 36), "GAME OVER");
        }
        else if (menang)
        {
            GUI.Label(new Rect(16, 88, 480, 36), "KAMU MENANG!");
        }
    }
}