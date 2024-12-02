using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0; // Índice de arma seleccionada
    [SerializeField] private Transform weaponHolder; // Holder donde las armas están instanciadas
    private InventoryManager inventoryManager;


    void Start()
    {
        // Obtener referencia al InventoryManager
        inventoryManager = GetComponent<InventoryManager>();

        // Suscribir al evento OnWeaponAdded
        if (inventoryManager != null)
        {
            inventoryManager.OnWeaponAdded += EquipWeaponFromInventory;
        }

        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        // Cambio de arma con el scroll del ratón
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= weaponHolder.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = weaponHolder.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        // Si el arma seleccionada ha cambiado, actualiza la visualización
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in weaponHolder) // Iterar sobre las armas en el holder
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true); // Activar la arma seleccionada
            }
            else
            {
                weapon.gameObject.SetActive(false); // Desactivar las demás armas
            }

            i++;
        }
    }

    void EquipWeaponFromInventory(Weapon weapon)
    {
        // Instanciamos el arma en el WeaponHolder
        GameObject weaponInstance = Instantiate(weapon.prefab, weaponHolder);
        // Aquí podrías agregar cualquier animación o configuración adicional para el arma instanciada
        weaponInstance.name = weapon.name; // Opcional: nombrar el objeto como el arma
        // Si es el primer arma, la activamos directamente
        if (weaponHolder.childCount == 1)
        {
            weaponInstance.SetActive(true);  // La primera arma debe estar visible
            selectedWeapon = 0;  // Seleccionar esta arma por defecto
        }
        else
        {
            // Si no es la primera arma, desactivamos el arma previamente seleccionada
            SelectWeapon();
        }
    }


}
