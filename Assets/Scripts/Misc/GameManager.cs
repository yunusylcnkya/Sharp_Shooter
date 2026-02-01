// Yazı (text) göstermek için
using TMPro;

// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Sahne (level) değiştirmek için
using UnityEngine.SceneManagement;

// Oyunun genel kurallarını yöneten ana sınıf
public class GameManager : MonoBehaviour
{
    // Ekranda kalan düşman sayısını gösteren yazı
    [SerializeField] TMP_Text enemiesLeftText;

    // Tüm düşmanlar bitince çıkan "Kazandın" yazısı
    [SerializeField] GameObject youWinText;

    // Sahnedeki kalan düşman sayısı
    int enemiesLeft = 0;

    // Yazının başında görünen sabit metin
    const string ENEMIES_LEFT_STRING = "Enemies Left: ";

    // Düşman sayısını artıran veya azaltan fonksiyon
    public void AdjustEnemiesLeft(int amount)
    {
        // Düşman sayısını değiştir
        enemiesLeft += amount;

        // Ekrandaki yazıyı güncelle
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        // Eğer hiç düşman kalmadıysa
        if (enemiesLeft <= 0)
        {
            // Kazandın yazısını göster
            youWinText.SetActive(true);
        }
    }

    // Yeniden başlat butonuna basınca çalışır
    public void RestartLevelButton()
    {
        // Şu anki sahnenin numarasını al
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        // Aynı sahneyi tekrar yükle
        SceneManager.LoadScene(currentScene);
    }

    // Oyundan çık butonuna basınca çalışır
    public void QuitButton()
    {
        // Unity Editöründe çalışmaz diye uyarı yazar
        Debug.LogWarning("Does not work in the Unity Editor!  You silly goose!");

        // Oyunu tamamen kapatır
        Application.Quit();
    }
}

