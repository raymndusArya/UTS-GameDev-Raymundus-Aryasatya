using UnityEngine;

// Inheritance: ZombieBiasa mewarisi semua field & method dari Enemy
// (hp, ms, JarakDeteksi, Kejar(), KenaDamage(), state machine, dst)
public class ZombieBiasa : Enemy
{
    // Zombie biasa tidak mengubah perilaku apa pun,
    // hanya menggunakan semua yang sudah ada di Enemy (class parent).
}