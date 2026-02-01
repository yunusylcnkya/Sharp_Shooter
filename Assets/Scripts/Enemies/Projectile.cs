// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Mermi / lazer gibi giden bir nesneyi yöneten sınıf
public class Projectile : MonoBehaviour
{
    // Merminin ne kadar hızlı gideceği
    [SerializeField] float speed = 30f;

    // Mermi bir şeye çarpınca çıkan efekt
    [SerializeField] GameObject projectileHitVFX;

    // Mermiyi hareket ettirmek için kullanılan fizik parçası
    Rigidbody rb;

    // Merminin vereceği hasar
    int damage;

    // Oyun başlarken çalışan fonksiyon
    void Awake()
    {
        // Bu objenin Rigidbody’sini alıyoruz
        rb = GetComponent<Rigidbody>();
    }

    // Awake’den sonra çalışan fonksiyon
    void Start()
    {
        // Mermiyi baktığı yöne doğru hızla fırlat
        rb.linearVelocity = transform.forward * speed;
    }

    // Mermi oluşturulurken hasarını ayarlamak için
    public void Init(int damage)
    {
        // Gelen hasar değerini kaydet
        this.damage = damage;
    }

    // Mermi bir şeye değdiğinde çalışır
    void OnTriggerEnter(Collider other)
    {
        // Çarpılan şey oyuncu mu diye bak
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        // Eğer oyuncuysa canını azalt
        playerHealth?.TakeDamage(damage);

        // Çarpma efektini oluştur
        Instantiate(projectileHitVFX, transform.position, Quaternion.identity);

        // Mermiyi sahneden sil
        Destroy(this.gameObject);
    }
}
