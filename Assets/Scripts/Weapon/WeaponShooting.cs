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
    private InventoryManager inventoryManager;
    private WeaponSwitching weaponSwitching;
    private PlayerHUD playerHUD;

    public bool canReload;
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
        playerHUD = GetComponent<PlayerHUD>();

        // Carga el sonido desde Resources
        shootClip = Resources.Load<AudioClip>("Audio/AutoGun_3p_02");

        if (shootClip == null)
        {
            Debug.LogError("No se encontr� el audio de disparo en Resources/Sounds/ShootSound");
        }

        primaryCurrentAmmo = 0;
        primaryCurrenttAmmoStorage = 0;




    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.PlayerAttacked())
        {
            Shooting();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload(weaponSwitching.SetSelectdWeapon());
        }
    }

    void Shooting()
    {
           
        CheckCanShoot(weaponSwitching.SetSelectdWeapon());

        if (canShoot /*&& canReload*/)
        {

            Weapon currentWeapon = inventoryManager.GetItem(weaponSwitching.SetSelectdWeapon());

            // Verificar si el tiempo actual es mayor o igual al tiempo del pr�ximo disparo permitido
            if (Time.time >= nextShootTime)
            {
                
                    AudioManager.Instance.PlaySFX(shootClip, 0.2f);
                    // Instanciar el bullet en la posici�n del punto de disparo
                    GameObject bullet = BulletPool.Instance.RequestBullet();
                    bullet.transform.position = weaponSwitching.currentWeaponBarrel.position;
                    bullet.transform.rotation = weaponSwitching.currentWeaponBarrel.rotation;
                    bullet.SetActive(true);

                    RayCastShoot(currentWeapon);

                    Vector3 direction = (targetPoint - weaponSwitching.currentWeaponBarrel.position).normalized;

                    // Debug para inspeccionar la direcci�n
                    Debug.DrawRay(weaponSwitching.currentWeaponBarrel.position, direction * 5, Color.red, 2f);

                    // Establecer la direcci�n de la bala
                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    bulletScript.SetDirection(direction);

                    // Actualizar el tiempo del pr�ximo disparo permitido
                    nextShootTime = Time.time + currentWeapon.fireRate;

                    UseAmmo((int)currentWeapon.weaponStyle, 1, 0);

          
            }           
        }


    }

    void RayCastShoot(Weapon currentWeapon)
    {
        // Calcular la direcci�n hacia el centro de la pantalla
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, currentWeapon.range))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000); // Un punto lejano en la direcci�n del ray
        }

        Instantiate(currentWeapon.particle[0], weaponSwitching.currentWeaponBarrel);
        Instantiate(currentWeapon.particle[1], weaponSwitching.currentWeaponBarrel);
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
                playerHUD.UpdateWeaponAmmoUI(primaryCurrentAmmo, primaryCurrenttAmmoStorage);
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
            playerHUD.UpdateWeaponAmmoUI(primaryCurrentAmmo, primaryCurrenttAmmoStorage);
            CheckCanShoot(slot);

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


    public void addAmmo(int slot,int currentAmmoAdd, int currentStoredAmmoAdded )
    {
        //Primary
        if (slot == 0)
        {

                primaryCurrentAmmo += currentAmmoAdd;
                primaryCurrenttAmmoStorage += currentStoredAmmoAdded;
                playerHUD.UpdateWeaponAmmoUI(primaryCurrentAmmo, primaryCurrenttAmmoStorage);

        }
    }

    private void Reload(int slot)
    {
        /*if (canReload == true)
        {*/
            if (slot == 0)
            {
                int ammoToReload = inventoryManager.GetItem(slot).magazineSize - primaryCurrentAmmo;

            if (primaryCurrenttAmmoStorage >= ammoToReload)
                {
                    if (primaryCurrentAmmo == inventoryManager.GetItem(slot).magazineSize)
                    {
                        Debug.Log("Magazine is already full");
                        return;
                    }    

                    addAmmo(slot,ammoToReload,0);
                    UseAmmo(slot, 0,ammoToReload);
                    primaryMagazineIsEmpty = false;
                    CheckCanShoot(slot);
                }
                else
                    Debug.Log("Not enaugh ammo to reload");


            }
    /*    }
        else
            Debug.Log("Can't reload at the moment");*/
    }

}
