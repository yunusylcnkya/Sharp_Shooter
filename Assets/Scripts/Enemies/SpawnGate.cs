// Zamanla çalışan şeyler (bekleme, döngü) için
using System.Collections;

// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Robotları belli aralıklarla doğuran kapı
public class SpawnGate : MonoBehaviour
{
    // Oluşturulacak robot
    [SerializeField] GameObject robotPrefab;

    // Kaç saniyede bir robot çıksın
    [SerializeField] float spawnTime = 5f;

    // Robotun çıkacağı yer
    [SerializeField] Transform spawnPoint;

    // Oyuncunun can scripti
    PlayerHealth player;

    // Oyun başlarken çalışan fonksiyon
    void Start()
    {
        // Sahnedeki oyuncuyu bul
        player = FindFirstObjectByType<PlayerHealth>();

        // Robot çıkarma döngüsünü başlat
        StartCoroutine(SpawnRoutine());
    }

    // Sürekli robot çıkaran döngü
    IEnumerator SpawnRoutine()
    {
        // Oyuncu hayattayken devam et
        while (player)
        {
            // Yeni bir robot oluştur
            Instantiate(robotPrefab, spawnPoint.position, transform.rotation);

            // Bir süre bekle
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
