// Bekleme ve tekrar eden işlemler için
using System.Collections;

// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Oyuncuya ateş eden otomatik taret (sabit silah)
public class Turret : MonoBehaviour
{
    // Ateş edilecek mermi / lazer prefabı
    [SerializeField] GameObject projectilePrefab;

    // Taretin dönen kafası
    [SerializeField] Transform turretHead;

    // Oyuncunun hedef alınacak noktası (göğüs gibi)
    [SerializeField] Transform playerTargetPoint;

    // Merminin çıkacağı nokta
    [SerializeField] Transform projectileSpawnPoint;

    // Kaç saniyede bir ateş etsin
    [SerializeField] float fireRate = 2f;

    // Merminin vereceği hasar
    [SerializeField] int damage = 2;

    // Oyuncunun can scripti
    PlayerHealth player;

    // Oyun başlarken çalışan fonksiyon
    void Start()
    {
        // Sahnedeki oyuncuyu bul
        player = FindFirstObjectByType<PlayerHealth>();

        // Ateş etme döngüsünü başlat
        StartCoroutine(FireRoutine());
    }

    // Oyun her karede burayı çalıştırır
    void Update()
    {
        // Taret kafasını sürekli oyuncuya doğru çevir
        turretHead.LookAt(playerTargetPoint);
    }

    // Belirli aralıklarla ateş eden döngü
    IEnumerator FireRoutine()
    {
        // Oyuncu hayattayken devam et
        while (player)
        {
            // Ateş etmeden önce bekle
            yield return new WaitForSeconds(fireRate);

            // Yeni bir mermi oluştur
            Projectile newProjectile = Instantiate(
                projectilePrefab,
                projectileSpawnPoint.position,
                Quaternion.identity
            ).GetComponent<Projectile>();

            // Mermiyi oyuncuya doğru çevir
            newProjectile.transform.LookAt(playerTargetPoint);

            // Merminin hasarını ayarla
            newProjectile.Init(damage);
        }
    }
}
