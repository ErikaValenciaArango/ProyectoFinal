using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Referencia al punto de disparo
    [SerializeField] private Transform shootPoint;



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

    [SerializeField] private int primaryCurrentAmmo;
    [SerializeField] private int primaryCurrenttAmmoStorage;

    //At moment
    Vector3 targetPoint;

    private void Start()
    {
        inputManager = InputManager.Instance;

        //Recursos del objeto
        player = GameObject.FindGameObjectWithTag("Player");
        inventoryManager = player.GetComponent<InventoryManager>();
        weaponSwitching = player.GetComponent<WeaponSwitching>();

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
                Debug.Log(primaryCurrentAmmo);
                Debug.Log(primaryCurrenttAmmoStorage);
    }

    void Shooting()
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
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = shootPoint.rotation;
                bullet.SetActive(true);

                RayCastShoot(currentWeapon);

                 Vector3 direction = (targetPoint - shootPoint.position).normalized;

                // Debug para inspeccionar la dirección
                Debug.DrawRay(shootPoint.position, direction * 5, Color.red, 2f);

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
        }
        else
        {
            targetPoint = ray.GetPoint(1000); // Un punto lejano en la dirección del ray
        }

        Instantiate(currentWeapon.particle[0], shootPoint);
        Instantiate(currentWeapon.particle[1], shootPoint);
    }


    void UseAmmo(int slot, int currentAmmoUsed, int currentStoredAmmoUsed)
    {
        //Primary
        if (slot == 0)
        {
            primaryCurrentAmmo -= currentAmmoUsed;
            primaryCurrenttAmmoStorage -= currentAmmoUsed;
        }
    }

    public void InitAmmo(int slot, Weapon weapon)
    {
        //Primary
        if (slot == 0)
        {

            primaryCurrentAmmo = weapon.magazineSize;
            primaryCurrenttAmmoStorage = weapon.storedAmmo;
            Debug.Log($"Usando munición: AmmoActual {primaryCurrentAmmo}, StoredAmmo {primaryCurrenttAmmoStorage}");
        }
    }
}
