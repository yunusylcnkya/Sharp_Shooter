// Unity’nin hazır FPS oyuncu kontrol sistemi
using StarterAssets;

// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Unity’nin yapay zekâ yürüyüş sistemi (NavMesh)
using UnityEngine.AI;

// Oyuncuyu takip eden robot düşmanı yöneten sınıf
public class Robot : MonoBehaviour
{
    // Oyuncu karakteri
    FirstPersonController player;

    // Robotun kendi kendine yürümesini sağlayan sistem
    NavMeshAgent agent;

    // Oyuncunun etiket adı
    const string PLAYER_STRING = "Player";

    // Oyun başlarken çalışan fonksiyon
    void Awake()
    {
        // Robotun NavMeshAgent parçasını al
        agent = GetComponent<NavMeshAgent>();
    }

    // Awake’den sonra çalışan fonksiyon
    void Start()
    {
        // Sahnedeki oyuncuyu bul
        player = FindFirstObjectByType<FirstPersonController>();
    }

    // Oyun her karede burayı çalıştırır
    void Update()
    {
        // Eğer oyuncu yoksa hiçbir şey yapma
        if (!player) return;

        // Robotu oyuncunun olduğu yere doğru yürüt
        agent.SetDestination(player.transform.position);
    }

    // Robot bir şeye dokunduğunda çalışır
    void OnTriggerEnter(Collider other)
    {
        // Dokunulan şey oyuncuysa
        if (other.CompareTag(PLAYER_STRING))
        {
            // Robotun can scriptini al
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();

            // Robot kendini patlatsın
            enemyHealth.SelfDestruct();
        }
    }
}
