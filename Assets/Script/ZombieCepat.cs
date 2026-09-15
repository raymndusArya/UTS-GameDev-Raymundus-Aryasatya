using UnityEngine;

public class ZombieCepat : Enemy
{
    [Header("Zombie Cepat - Setting Khusus")]
    [SerializeField] private int attackDamageKecil = 5;
    [SerializeField] private float kecepatan = 4f;

    protected override void Start()
    {
        base.Start(); // tetap jalankan logika Start() bawaan dari Enemy

        // Override kecepatan gerak, ZombieCepat lebih gesit dari zombie biasa
        ms = kecepatan;
    }

    // Polymorphism: override method Serang() dari Enemy (yang ditandai 'virtual')
    // Zombie cepat menyerang lebih sering tapi damage-nya lebih kecil  
    public override void Serang()
    {
        if (player == null) return;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth != null && playerHealth.IsDead())
        {
            return;
        }

        Debug.Log("ZombieCepat menyerang cepat!");

        if (playerHealth != null)
        {
            playerHealth.KenaDamage(attackDamageKecil);
        }
    }
}