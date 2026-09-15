using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp = 100;
    private int currentHp;
    private bool isDead = false;
    public event Action OnPlayerMati;

    void Awake()
    {
        currentHp = maxHp;
    }

    public void KenaDamage(int jumlah)
    {
        // Kalau sudah mati, jangan proses damage lagi
        if (isDead) return;
        currentHp -= jumlah;

        // Clamp biar HP tidak pernah minus
        currentHp = Mathf.Max(currentHp, 0);
        Debug.Log("Player HP: " + currentHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log("Player Mati!");

        // Pancarkan event ke siapa pun yang sedang "mendengarkan"
        OnPlayerMati?.Invoke();

        // Matikan movement biar player gak bisa gerak lagi
        PlayerMovement pm = GetComponent<PlayerMovement>();
        if (pm != null) pm.enabled = false;

    }

    public bool IsDead()
    {
        return isDead;
    }

    public int GetCurrentHp()
    {
        return currentHp;
    }
}