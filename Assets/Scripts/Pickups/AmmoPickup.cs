// Unity oyun motorunun temel özellikleri
using UnityEngine;

// Yerden mermi toplanmasını sağlayan sınıf
// Pickup sınıfından miras alır (yani onun özelliklerini kullanır)
public class AmmoPickup : Pickup
{
    // Alındığında kaç mermi versin
    [SerializeField] int ammoAmount = 100;

    // Oyuncu bu objeyi aldığında otomatik çalışır
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        // Oyuncunun silahına mermi ekle
        activeWeapon.AdjustAmmo(ammoAmount);
    }
}
