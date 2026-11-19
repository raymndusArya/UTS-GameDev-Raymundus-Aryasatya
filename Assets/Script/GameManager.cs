using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalKoin;
    private int koinTerkumpul = 0;
    private int potionTerkumpul = 0;
    private int jumlahZombieMati = 0;

    void Start()
    {
        totalKoin = GameObject.FindGameObjectsWithTag("Koin").Length;
    }

    void SaatZombieMati(Enemy zombie)
    {
        jumlahZombieMati++;
        Debug.Log("GameManager dengar event. Zombie mati: " + jumlahZombieMati + " (" + zombie.name + ")");
    }

    public void AmbilKoin()
    {
        koinTerkumpul++;
        if (koinTerkumpul == totalKoin)
        {
            Menang();
        }
    }

    public void AmbilPotion()
    {
        potionTerkumpul++;
    }

    void Menang()
    {
        Debug.Log("KAMU MENANG!");
    }

    // void OnGUI()
    // {
    //     GUI.skin.label.fontSize = 30;
    //     GUI.Label(new Rect(16, 36, 480, 36), "Koin: " + koinTerkumpul + " / " + totalKoin);
    //     GUI.Label(new Rect(16, 72, 480, 36), "Potion: " + potionTerkumpul );
    //     GUI.Label(new Rect(16, 108, 480, 36), "Zombie mati: " + jumlahZombieMati);
    // }
}
