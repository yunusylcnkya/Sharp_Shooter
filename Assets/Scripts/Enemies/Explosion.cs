// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Patlama etkisini yöneten sınıf
public class Explosion : MonoBehaviour
{
    // Patlamanın etki alanı (ne kadar uzağa zarar verir)
    [SerializeField] float radius = 1.5f;

    // Patlama oyuncudan kaç can götürsün
    [SerializeField] int damage = 3;

    // Obje sahneye gelir gelmez çalışır
    void Start()
    {
        // Patlamayı başlat
        Explode();
    }

    // Sahne görünümünde (Scene) kırmızı daire çizer
    // Bu sadece geliştirici için, oyuncu görmez
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Patlama işlemini yapan fonksiyon
    void Explode()
    {
        // Patlama alanının içindeki tüm nesneleri bul
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        // Bulunan nesneleri tek tek kontrol et
        foreach (Collider hitCollider in hitColliders)
        {
            // Bu nesne oyuncu mu diye bak
            PlayerHealth playerhealth = hitCollider.GetComponent<PlayerHealth>();

            // Oyuncu değilse geç
            if (!playerhealth) continue;

            // Oyuncuya hasar ver
            playerhealth.TakeDamage(damage);

            // Sadece bir oyuncuya zarar versin, döngüyü bitir
            break;
        }
    }
}
