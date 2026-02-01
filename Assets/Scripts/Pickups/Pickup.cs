// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Yerden alınabilen tüm eşyaların ana sınıfı
// abstract = Bu sınıf tek başına kullanılmaz
public abstract class Pickup : MonoBehaviour
{
    // Eşyanın kendi etrafında dönme hızı
    [SerializeField] float rotationSpeed = 100f;

    // Oyuncunun etiket adı
    const string PLAYER_STRING = "Player";

    // Oyun her karede burayı çalıştırır
    void Update()
    {
        // Eşyayı sürekli döndür (dikkat çeksin diye)
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    // Oyuncu bu eşyaya değdiğinde çalışır
    void OnTriggerEnter(Collider other)
    {
        // Dokunan şey oyuncu mu?
        if (other.CompareTag(PLAYER_STRING))
        {
            // Oyuncunun elindeki silah sistemini bul
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();

            // Eşyaya özel etkiyi çalıştır
            OnPickup(activeWeapon);

            // Eşyayı sahneden sil
            Destroy(this.gameObject);
        }
    }

    // Bu fonksiyon her pickup için farklı çalışır
    // (mermi verir, can verir, güç verir gibi)
    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
