// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Unity’nin hazır FPS kontrol sistemi
using StarterAssets;

// Sinematik kamera sistemi
using Cinemachine;

// Yazı (mermi sayısı gibi) göstermek için
using TMPro;

// Oyuncunun elindeki silahı yöneten sınıf
public class ActiveWeapon : MonoBehaviour
{
    // Oyunun başında oyuncunun elinde olacak silah
    [SerializeField] WeaponSO startingWeapon;

    // Oyuncuyu takip eden ana kamera
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;

    // Silahın kendi kamerası
    [SerializeField] Camera weaponCamera;

    // Zoom yapınca ekrana gelen karartma efekti
    [SerializeField] GameObject zoomVignette;

    // Ekranda görünen mermi sayısı yazısı
    [SerializeField] TMP_Text ammoText;

    // Şu an kullanılan silahın bilgileri
    WeaponSO currentWeaponSO;

    // Silahın ateş etme animasyonunu oynatır
    Animator animator;

    // Mouse ve klavye girdilerini alır
    StarterAssetsInputs starterAssetsInputs;

    // Oyuncunun yürüme ve dönme kontrolü
    FirstPersonController firstPersonController;

    // Sahnedeki gerçek silah objesi
    Weapon currentWeapon;

    // Animatördeki "Shoot" animasyonunun adı
    const string SHOOT_STRING = "Shoot";

    // Son atıştan sonra geçen süre
    float timeSinceLastShot = 0f;

    // Kameranın normal görüş açısı
    float defaultFOV;

    // Oyuncunun normal dönme hızı
    float defaultRotationSpeed;

    // Şu anki mermi sayısı
    int currentAmmo;

    // Oyun başlarken çalışan fonksiyon
    void Awake()
    {
        // Oyuncu girdilerini buluyoruz
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();

        // Oyuncunun hareket scriptini alıyoruz
        firstPersonController = GetComponentInParent<FirstPersonController>();

        // Animatörü alıyoruz
        animator = GetComponent<Animator>();

        // Kameranın normal görüş açısını kaydediyoruz
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;

        // Oyuncunun normal dönme hızını kaydediyoruz
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    // Awake’den sonra çalışan fonksiyon
    void Start()
    {
        // Oyuncuya başlangıç silahını veriyoruz
        SwitchWeapon(startingWeapon);

        // Silahı tam dolu şarjörle başlatıyoruz
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    // Oyun her karede burayı çalıştırır
    void Update()
    {
        // Ateş etme kontrolü
        HandleShoot();

        // Zoom kontrolü
        HandleZoom();
    }

    // Mermi sayısını artırır veya azaltır
    public void AdjustAmmo(int amount)
    {
        // Mermiyi ekle veya çıkar
        currentAmmo += amount;

        // Mermi sayısı şarjörü geçemesin
        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }

        // Ekranda mermi sayısını göster (01, 02 gibi)
        ammoText.text = currentAmmo.ToString("D2");
    }

    // Silah değiştirme fonksiyonu
    public void SwitchWeapon(WeaponSO weaponSO)
    {
        // Eğer elde silah varsa onu sil
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        // Yeni silahı oluştur
        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();

        // Yeni silahı aktif silah yap
        currentWeapon = newWeapon;
        this.currentWeaponSO = weaponSO;

        // Yeni silahın mermisini doldur
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    // Ateş etme işlemlerini kontrol eder
    void HandleShoot()
    {
        // Zamanı say
        timeSinceLastShot += Time.deltaTime;

        // Ateş tuşuna basılmamışsa çık
        if (!starterAssetsInputs.shoot) return;

        // Ateş edebilirsek ve mermi varsa
        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            // Silah ateş etsin
            currentWeapon.Shoot(currentWeaponSO);

            // Ateş animasyonunu oynat
            animator.Play(SHOOT_STRING, 0, 0f);

            // Zamanı sıfırla
            timeSinceLastShot = 0f;

            // Bir mermi eksilt
            AdjustAmmo(-1);
        }

        // Silah otomatik değilse tuşu bırak
        if (!currentWeaponSO.isAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    // Zoom yapmayı kontrol eder
    void HandleZoom()
    {
        // Bu silah zoom yapamıyorsa çık
        if (!currentWeaponSO.CanZoom) return;

        // Zoom tuşuna basıldıysa
        if (starterAssetsInputs.zoom)
        {
            // Kamerayı yakınlaştır
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;

            // Zoom efekti aç
            zoomVignette.SetActive(true);

            // Oyuncunun dönme hızını azalt
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            // Zoom bırakıldıysa her şeyi eski haline getir
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;
            zoomVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }
}
