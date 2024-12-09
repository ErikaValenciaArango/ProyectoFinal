using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public GameObject currentWeapon = null;
    public Transform weaponHolder = null; // Holder donde las armas están instanciadas
    
    private InventoryManager inventoryManager;
    private Animator animPlayer;

    private int selectedWeapon = 1; // Inicialmente no hay arma seleccionada
    private float lastScrollTime = 0f; // Registro del último cambio con la rueda del ratón
    private float scrollCooldown = 0.07f; // Tiempo mínimo entre cambios (en segundos)

    //Try
    private PlayerHUD playerHUD;

    void Start()
    {
        // Obtener referencia al InventoryManager
        inventoryManager = GetComponent<InventoryManager>();
        animPlayer = GetComponent<Animator>();
        playerHUD = GetComponent<PlayerHUD>();
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        int previousSelectedWeapon = selectedWeapon;
        int newSelectedWeapon = selectedWeapon;

        // Seleccionar arma con teclas numéricas
        if (Input.GetKeyDown(KeyCode.Alpha1)) newSelectedWeapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) newSelectedWeapon = 0;

        // Seleccionar arma con la rueda del ratón (solo si ha pasado el cooldown)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Time.time - lastScrollTime > scrollCooldown)
        {
            if (scroll > 0f) newSelectedWeapon = (selectedWeapon + 1) % inventoryManager.InventorySize;
            if (scroll < 0f) newSelectedWeapon = (selectedWeapon - 1 + inventoryManager.InventorySize) % inventoryManager.InventorySize;

            lastScrollTime = Time.time; // Actualizar el tiempo del último cambio
        }


        // Verificar si el nuevo slot no está vacío
        if (inventoryManager.GetItem(newSelectedWeapon) == null)
        {
            return;
        }

        // Si la selección cambió, equipar el arma correspondiente
        if (previousSelectedWeapon != newSelectedWeapon)
        {
            selectedWeapon = newSelectedWeapon;
            UnequipWeapon();
            EquipWeapon(inventoryManager.GetItem(selectedWeapon));
        }
    }


     public void EquipWeapon(Weapon weapon)
    {

        if (weapon == null) return;

        selectedWeapon = (int)weapon.weaponStyle;
        // Actualizar animaciones
        animPlayer.SetInteger("weaponType", (int)weapon.weaponType);
        playerHUD.UpdateWeaponUI(weapon);
    }

    public  void UnequipWeapon()
    {
        animPlayer.SetTrigger("unequipWeapon");
    }

    public void InstantWeapon()
    {
        Destroy(currentWeapon);
        currentWeapon = Instantiate(inventoryManager.GetItem(selectedWeapon).prefab, weaponHolder);
    }

    public int SetSelectdWeapon()
    {
        return selectedWeapon;
    }

}
