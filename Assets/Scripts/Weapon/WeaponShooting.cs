using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    // Tiempo de cooldown entre disparos
    private float nextShootTime = 0f;

    private InputManager inputManager;

    private AudioClip shootClip;


    /// <summary>
    /// Identacion de municion y armas
    /// </summary>
    private GameObject player;
    private InventoryManager inventoryManager;
    private WeaponSwitching weaponSwitching;

    [SerializeField] private bool canShoot;
    [SerializeField] private int primaryCurrentAmmo;
    [SerializeField] private int primaryCurrenttAmmoStorage;
    [SerializeField] private bool primaryMagazineIsEmpty = false;

    //At moment
    Vector3 targetPoint;

    private void Start()
    {
        inputManager = InputManager.Instance;

        //Recursos del objeto
        inventoryManager = GetComponent<InventoryManager>();
        weaponSwitching = GetComponent<WeaponSwitching>();

        // Carga el sonido desde Resources
        shootClip = Resources.Load<AudioClip>("Audio/AutoGun_3p_02");

        if (shootClip == null)
        {
            Debug.LogError("No se encontró el audio de disparo en Resources/Sounds/ShootSound");
        }

        primaryCurrentAmmo = 0;
        primaryCurrenttAmmoStorage = 0;




    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
           
        CheckCanShoot(weaponSwitching.SetSelectdWeapon());

        if (canShoot)
        {

            Weapon currentWeapon = inventoryManager.GetItem(weaponSwitching.SetSelectdWeapon());

            // Verificar si el tiempo actual es mayor o igual al tiempo del próximo disparo permitido
            if (Time.time >= nextShootTime)
            {
                if (inputManager.PlayerAttacked())
                {
                    AudioManager.Instance.PlaySFX(shootClip, 0.2f);
                    // Instanciar el bullet en la posición del punto de disparo
                    GameObject bullet = BulletPool.Instance.RequestBullet();
                    bullet.transform.position = weaponSwitching.weaponHolder.position;
                    bullet.transform.rotation = weaponSwitching.weaponHolder.rotation;
                    bullet.SetActive(true);

                    RayCastShoot(currentWeapon);

                    Vector3 direction = (targetPoint - weaponSwitching.weaponHolder.position).normalized;

                    // Debug para inspeccionar la dirección
                    Debug.DrawRay(weaponSwitching.weaponHolder.position, direction * 5, Color.red, 2f);

                    // Establecer la dirección de la bala
                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    bulletScript.SetDirection(direction);

                    // Actualizar el tiempo del próximo disparo permitido
                    nextShootTime = Time.time + currentWeapon.fireRate;

                    UseAmmo((int)currentWeapon.weaponStyle, 1, 0);

                }
            }           
        }
        else
            Debug.Log("Not enough ammo in magazine");


    }

    void RayCastShoot(Weapon currentWeapon)
    {
        // Calcular la dirección hacia el centro de la pantalla
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, currentWeapon.range))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000); // Un punto lejano en la dirección del ray
        }

        Instantiate(currentWeapon.particle[0], weaponSwitching.weaponHolder);
        Instantiate(currentWeapon.particle[1], weaponSwitching.weaponHolder);
    }


    void UseAmmo(int slot, int currentAmmoUsed, int currentStoredAmmoUsed)
    {
        //Primary
        if (slot == 0)
        {
            if (primaryCurrentAmmo <= 0)
            {
                primaryMagazineIsEmpty = true;
                CheckCanShoot(weaponSwitching.SetSelectdWeapon());
            }
            else
            {
                primaryCurrentAmmo -= currentAmmoUsed;
                primaryCurrenttAmmoStorage -= currentStoredAmmoUsed;
            }
        }
    }

    public void InitAmmo(int slot, Weapon weapon)
    {
        //Primary
        if (slot == 0)
        {

            primaryCurrentAmmo = weapon.magazineSize;
            primaryCurrenttAmmoStorage = weapon.storedAmmo;
        }
    }

    private void CheckCanShoot( int solt)
    {
        if (solt == 0)
        {
            if (primaryMagazineIsEmpty)
            {
                canShoot = false;
            }
            else 
            {
                canShoot = true;
            }
        }
    }
}
