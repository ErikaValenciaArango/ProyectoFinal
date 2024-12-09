using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private WeaponUI WeaponUI;

    public void UpdateWeaponUI(Weapon newWeapon)
    {
        WeaponUI.UpdateInfo(newWeapon.magazineSize, newWeapon.storedAmmo);
    }

    public void UpdateWeaponAmmoUI(int current, int storedAmmo)
    {
        WeaponUI.UpdateAmmoUI(current, storedAmmo);
    }
}
