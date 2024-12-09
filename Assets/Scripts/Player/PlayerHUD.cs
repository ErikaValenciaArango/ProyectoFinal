using UnityEngine;


public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private WeaponUI WeaponUI;
    [SerializeField] private PlayerUI PlayerUI;




    public void UpdateWeaponUI(Weapon newWeapon)
    {
        WeaponUI.UpdateInfo(newWeapon.magazineSize, newWeapon.storedAmmo);
    }

    public void UpdateWeaponAmmoUI(int current, int storedAmmo)
    {
        WeaponUI.UpdateAmmoUI(current, storedAmmo);
    }

    public void UpdateHealth (int currentHealth, int maxHealth)
    {
        //Aca se manipula la vida del personaje
        PlayerUI.ChekHealth(currentHealth, maxHealth);
    }
}
