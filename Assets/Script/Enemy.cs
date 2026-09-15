using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 100;
    public float ms = 2f;

    protected Transform player;

    [Header("State Machine")]
    [SerializeField] private float JarakDeteksi = 6f;
    [SerializeField] private float JarakSerang = 1.5f;
    [SerializeField] private float JedaSerang = 1f;

    [Header("Attack")]
    [SerializeField] private int attackDamage = 10;

    // Delegate & Event: pemancar sinyal "zombie ini sudah mati"
    // GameManager (atau script lain) tinggal subscribe ke event ini
    public event Action<Enemy> OnZombieMati;

    // State Now
    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;
    private bool isDead = false;

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        // supaya serangan pertama tidak perlu nunggu JedaSerang penuh
        waktuSerangTerakhir = -JedaSerang;
    }

    void Update()
    {
        if (isDead) return;

        PeriksaTransisi();

        switch (state)
        {
            case StateZombie.IDLE: PerilakuIdle(); break;
            case StateZombie.PATROL: PerilakuPatrol(); break;
            case StateZombie.CHASE: PerilakuChase(); break;
            case StateZombie.ATTACK: PerilakuAttack(); break;
        }
    }

    public void KenaDamage(int jumlah)
    {
        if (isDead) return;

        hp -= jumlah;

        if (hp <= 0)
        {
            Mati();
        }
    }

    void Mati()
    {
        isDead = true;

        Debug.Log(gameObject.name + " mati");

        // Pancarkan event, kirim referensi diri sendiri (this)
        // biar penerima tahu zombie mana yang mati
        OnZombieMati?.Invoke(this);

        Destroy(gameObject);
    }

    public float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak <= JarakSerang)
        {
            state = StateZombie.ATTACK;
        }
        else if (jarak <= JarakDeteksi)
        {
            state = StateZombie.CHASE;
        }
        else
        {
            state = StateZombie.PATROL;
        }
    }

    public void Kejar()
    {
        if (player == null) return;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null && playerHealth.IsDead()) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        if (player == null) return;
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        // Kalau player sudah mati, enemy tidak menyerang lagi
        if (playerHealth != null && playerHealth.IsDead()) return;

        Debug.Log("Enemy Menyerang");

        if (playerHealth != null)
        {
            playerHealth.KenaDamage(attackDamage);
        }
    }

    // Dipanggil otomatis oleh Unity saat collider enemy menabrak collider lain.
    // Karena ada di class induk, SEMUA turunan zombie ikut punya perilaku ini.
    void PerilakuIdle()
    {
        Debug.Log("Enemy IDLE");
    }

    void PerilakuPatrol()
    {
        Debug.Log("Enemy PATROL");
    }

    void PerilakuChase()
    {
        Kejar();
        Debug.Log("Enemy CHASE");
    }

    void PerilakuAttack()
    {
        Debug.Log("Enemy ATTACK");

        if (Time.time >= waktuSerangTerakhir + JedaSerang)
        {
            Serang();
            waktuSerangTerakhir = Time.time;
        }
    }
}