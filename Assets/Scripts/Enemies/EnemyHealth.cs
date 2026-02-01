// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Düşmanın canını ve yok olmasını yöneten sınıf
public class EnemyHealth : MonoBehaviour
{
    // Düşman yok olunca çıkan patlama efekti
    [SerializeField] GameObject robotExplosionVFX;

    // Düşmanın oyuna kaç canla başlayacağı
    [SerializeField] int startingHealth = 3;

    // Düşmanın şu anki canı
    int currentHealth;

    // Oyundaki genel kuralları yöneten GameManager
    GameManager gameManager;

    // Oyun başlarken çalışan fonksiyon
    void Awake()
    {
        // Düşmanın canını başlangıç canına ayarla
        currentHealth = startingHealth;
    }

    // Awake’den sonra çalışan fonksiyon
    void Start()
    {
        // Sahnedeki GameManager’ı bul
        gameManager = FindFirstObjectByType<GameManager>();

        // Oyuna bir düşman eklendiğini söyle
        gameManager.AdjustEnemiesLeft(1);
    }

    // Düşman hasar aldığında çalışan fonksiyon
    public void TakeDamage(int amount)
    {
        // Düşmanın canını azalt
        currentHealth -= amount;

        // Eğer can biterse
        if (currentHealth <= 0)
        {
            // Oyundaki düşman sayısını bir azalt
            gameManager.AdjustEnemiesLeft(-1);

            // Düşmanı yok et
            SelfDestruct();
        }
    }

    // Düşmanın kendini yok etmesi
    public void SelfDestruct()
    {
        // Patlama efektini oluştur
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity);

        // Düşmanı sahneden sil
        Destroy(this.gameObject);
    }
}
