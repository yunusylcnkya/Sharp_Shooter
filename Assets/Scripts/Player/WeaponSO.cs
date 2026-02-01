// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Sağ tık → Create menüsünde bu silah dosyasını oluşturabilmemizi sağlar
[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]

// Bu sınıf bir SİLAH BİLGİ KARTI gibidir
// Silahın kendisi değil, özelliklerini tutar
public class WeaponSO : ScriptableObject
{
    // Oyunda görünen silah modeli (tabanca, tüfek vb.)
    public GameObject weaponPrefab;

    // Silah bir kere vurunca kaç can götürür
    public int Damage = 1;

    // Silahın ne kadar hızlı ateş edebileceği
    // Küçük sayı = daha hızlı ateş
    public float FireRate = .5f;

    // Kurşun bir yere çarpınca çıkan efekt (kıvılcım, kan vb.)
    public GameObject HitVFXPrefab;

    // Silah otomatik mi?
    // true = basılı tutunca tarar
    // false = her basışta bir kez ateş eder
    public bool isAutomatic = false;

    // Bu silah yakınlaştırma (zoom) yapabilir mi?
    public bool CanZoom = false;

    // Zoom yapınca kamera ne kadar yaklaşsın
    // Küçük değer = daha çok yakınlaşma
    public float ZoomAmount = 10f;

    // Zoom yaparken oyuncu ne kadar hızlı dönebilsin
    // Küçük değer = daha yavaş ve kontrollü dönüş
    public float ZoomRotationSpeed = .3f;

    // Şarjörde kaç mermi var
    public int MagazineSize = 12;
}
