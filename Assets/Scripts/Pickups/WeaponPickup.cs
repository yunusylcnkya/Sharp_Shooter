// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Yerden yeni bir silah alınmasını sağlayan sınıf
// Pickup sınıfından özellikleri miras alır
public class WeaponPickup : Pickup
{
    // Alındığında verilecek silahın bilgileri
    [SerializeField] WeaponSO weaponSO;

    // Oyuncu bu silahı aldığında çalışan fonksiyon
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        // Oyuncunun elindeki silahı değiştir
        activeWeapon.SwitchWeapon(weaponSO);
    }
}
