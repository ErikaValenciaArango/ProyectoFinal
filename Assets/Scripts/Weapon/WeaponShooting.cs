using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    // Tiempo de cooldown entre disparos
    private float nextShootTime = 0f;
    private float nextCutTime = 0f;


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

    /// <summary>
    /// Revisar uso mal de la programacion
    /// </summary>
    private Animator animate;

    private void Start()
    {
        inputManager = InputManager.Instance;

        //Recursos del objeto
        inventoryManager = GetComponent<InventoryManager>();
        weaponSwitching = GetComponent<WeaponSwitching>();
        playerHUD = GetComponent<PlayerHUD>();
        animate = GetComponent<Animator>();

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
        if (inputManager.PlayerAttacked())
        {
            Shooting();
            Cutting();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload(weaponSwitching.SetSelectdWeapon());
        }
    }


    void Cutting()
    {
        CheckCanShoot(weaponSwitching.SetSelectdWeapon());

        if (weaponSwitching.SetSelectdWeapon() == 1 && weaponSwitching.currentWeapon!=null)
        {
            Weapon currentWeapon = inventoryManager.GetItem(weaponSwitching.SetSelectdWeapon());
            if(Time.time >= nextCutTime)
            {
                RayCastShoot(currentWeapon);
            }

        }
    }



    /// <summary>
    /// Codigo dedicado al arma pistola
    /// </summary>
    void Shooting()
    {
           
        CheckCanShoot(weaponSwitching.SetSelectdWeapon());

        if (canShoot && weaponSwitching.SetSelectdWeapon() == 0)
        {

            Weapon currentWeapon = inventoryManager.GetItem(weaponSwitching.SetSelectdWeapon());

            // Verificar si el tiempo actual es mayor o igual al tiempo del próximo disparo permitido
            if (Time.time >= nextShootTime)
            {
                
                    AudioManager.Instance.PlaySFX(shootClip, 0.2f);
                    // Instanciar el bullet en la posición del punto de disparo
                    GameObject bullet = BulletPool.Instance.RequestBullet();
                    bullet.transform.position = weaponSwitching.currentWeaponBarrel.position;
                    bullet.transform.rotation = weaponSwitching.currentWeaponBarrel.rotation;
                    bullet.SetActive(true);

                    RayCastShoot(currentWeapon);

                    Vector3 direction = (targetPoint - weaponSwitching.currentWeaponBarrel.position).normalized;

                    // Debug para inspeccionar la dirección
                    Debug.DrawRay(weaponSwitching.currentWeaponBarrel.position, direction * 5, Color.red, 2f);

                    // Establecer la dirección de la bala
                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    bulletScript.SetDirection(direction);

                    // Actualizar el tiempo del próximo disparo permitido
                    nextShootTime = Time.time + currentWeapon.fireRate;

                    UseAmmo((int)currentWeapon.weaponStyle, 1, 0);

          
            }           
        }


    }

    void RayCastShoot(Weapon currentWeapon)
    {
        // Calcular la dirección hacia el centro de la pantalla
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, currentWeapon.range))
        {
            targetPoint = hit.point;
            if (hit.collider.CompareTag("Enemy"))
            {
                    CharacterStats enemyStats = hit.transform.GetComponent<CharacterStats>();
                    enemyStats.TakeDamage(currentWeapon.damage);
            }
        }
        else
        {
            targetPoint = ray.GetPoint(1000); // Un punto lejano en la dirección del ray
        }
        if (currentWeapon.name.Equals("Knife"))
        {
            animate.SetTrigger("Attack");
        }
        if(currentWeapon.name.Equals("Pistol"))
        {
            Instantiate(currentWeapon.particle[0], weaponSwitching.currentWeaponBarrel);
            Instantiate(currentWeapon.particle[1], weaponSwitching.currentWeaponBarrel);
        }
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
            primaryMagazineIsEmpty = false;
            CheckCanShoot(slot);
            playerHUD.UpdateWeaponAmmoUI(primaryCurrentAmmo, primaryCurrenttAmmoStorage);

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
