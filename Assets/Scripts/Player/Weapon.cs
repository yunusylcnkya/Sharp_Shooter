// Sinematik kamera sarsıntısı için kullanılan sistem
using Cinemachine;

// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Silahın nasıl ateş ettiğini yöneten sınıf
public class Weapon : MonoBehaviour
{
    // Silah ateş edince çıkan ışık ve efekt
    [SerializeField] ParticleSystem muzzleFlash;

    // Kurşunun hangi katmanlara çarpabileceğini belirler
    [SerializeField] LayerMask interactionLayers;

    // Kamera sarsıntısı yapmak için kullanılır
    CinemachineImpulseSource impulseSource;

    // Oyun başlarken çalışan fonksiyon
    void Awake()
    {
        // Bu objenin üzerindeki kamera sarsıntı sistemini alıyoruz
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Silah ateş ettiğinde çalışan fonksiyon
    public void Shoot(WeaponSO weaponSO)
    {
        // Namlu alevini oynat (ışık çıkması)
        muzzleFlash.Play();

        // Kamerayı hafif salla (geri tepme hissi)
        impulseSource.GenerateImpulse();

        // Kurşunun neye çarptığını tutacak değişken
        RaycastHit hit;

        // Kameranın baktığı yönde görünmez bir çizgi (kurşun) gönderiyoruz
        if (Physics.Raycast(
            Camera.main.transform.position,   // Kurşun kameradan çıkar
            Camera.main.transform.forward,    // Kameranın baktığı yöne gider
            out hit,                          // Bir şeye çarparsa buraya kaydedilir
            Mathf.Infinity,                  // Çok uzağa kadar gider
            interactionLayers,               // Sadece izin verilen katmanlara çarpar
            QueryTriggerInteraction.Ignore)) // Trigger objeleri görmezden gel
        {
            // Kurşunun çarptığı yere vurma efekti oluştur
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);

            // Çarpılan objenin düşman olup olmadığını kontrol et
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();

            // Eğer düşmansa canını azalt
            enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
