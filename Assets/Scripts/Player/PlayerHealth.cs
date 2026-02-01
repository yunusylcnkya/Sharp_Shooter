// Kameralar ve sinematik geçişler için kullanılan kütüphane
using Cinemachine;

// Unity’nin hazır FPS / TPS kontrol sistemleri
using StarterAssets;

// Unity’nin temel oyun motoru
using UnityEngine;

// Can barları gibi görsel UI elemanları için
using UnityEngine.UI;

// Oyuncunun canını yöneten sınıf
public class PlayerHealth : MonoBehaviour
{
    // Oyuncunun oyuna kaç canla başlayacağını belirler (1 ile 10 arası)
    [Range(1, 10)]
    [SerializeField] int startingHealth = 5;

    // Oyuncu ölünce geçilecek olan kamera
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;

    // Silahın bağlı olduğu kamera
    [SerializeField] Transform weaponCamera;

    // Ekrandaki kalkan / can göstergeleri (küçük ikonlar)
    [SerializeField] Image[] shieldBars;

    // Oyun bitince açılacak ekran (Game Over yazısı gibi)
    [SerializeField] GameObject gameOverContainer;

    // Oyuncunun şu anki canı
    int currentHealth;

    // Ölünce kamera önceliği (yüksek olunca bu kamera aktif olur)
    int gameOverVirtualCameraPriority = 20;

    // Oyun başlarken ilk çalışan fonksiyon
    void Awake()
    {
        // Oyuncunun canını başlangıç canına eşitliyoruz
        currentHealth = startingHealth;

        // Can göstergelerini ekrana uygun şekilde ayarlıyoruz
        AdjustShieldUI();
    }

    // Oyuncu hasar aldığında çalışır
    public void TakeDamage(int amount)
    {
        // Canı azaltıyoruz
        currentHealth -= amount;

        // Can azaldığı için ekrandaki ikonları güncelliyoruz
        AdjustShieldUI();

        // Eğer can 0 veya daha küçükse oyuncu ölür
        if (currentHealth <= 0)
        {
            PlayerGameOver();
        }
    }

    // Oyuncu öldüğünde çalışan fonksiyon
    void PlayerGameOver()
    {
        // Silah kamerasını oyuncudan ayırıyoruz
        weaponCamera.parent = null;

        // Ölüm kamerasını aktif hale getiriyoruz
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority;

        // Game Over ekranını açıyoruz
        gameOverContainer.SetActive(true);

        // Oyuncunun klavye ve mouse kontrolünü kapatıyoruz
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false);

        // Oyuncu karakterini oyundan siliyoruz
        Destroy(this.gameObject);
    }

    // Can göstergelerini ayarlayan fonksiyon
    void AdjustShieldUI()
    {
        // Tüm can ikonlarını tek tek kontrol ediyoruz
        for (int i = 0; i < shieldBars.Length; i++)
        {
            // Eğer bu ikon oyuncunun canı içindeyse göster
            if (i < currentHealth)
            {
                shieldBars[i].gameObject.SetActive(true);
            }
            // Değilse gizle
            else
            {
                shieldBars[i].gameObject.SetActive(false);
            }
        }
    }
}
