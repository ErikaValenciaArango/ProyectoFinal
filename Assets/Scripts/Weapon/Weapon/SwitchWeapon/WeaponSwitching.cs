using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public Transform weaponHolder; // Holder donde las armas están instanciadas
    private InventoryManager inventoryManager;
    private Animator animPlayer;


    //Weapon shose
    [SerializeField] Weapon defaultHandsWeapon = null;
    public int selectedWeapon = 2; // Índice de arma seleccionada
    public GameObject currentWeapon = null;

    void Start()
    {
        // Obtener referencia al InventoryManager
        inventoryManager = GetComponent<InventoryManager>();
        animPlayer = GetComponent<Animator>();

        inventoryManager.AddItem(defaultHandsWeapon);
        EquipWeapon(inventoryManager.GetItem(1));
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        Debug.Log(selectedWeapon);

        if(Input.GetKeyDown(KeyCode.Alpha1) && selectedWeapon != 1)
        {
            UnequipWeapon();
            EquipWeapon (inventoryManager.GetItem(1));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && selectedWeapon != 0)
        {
            UnequipWeapon();
            EquipWeapon(inventoryManager.GetItem(0));
        }

    }

    void EquipWeapon(Weapon weapon)
    {
        animPlayer.SetInteger("weaponType", (int)weapon.weaponType);
        selectedWeapon = (int)weapon.weaponStyle;

    }

    void UnequipWeapon()
    {
        animPlayer.SetTrigger("unequipWeapon");
    }

    public void InstantWeapon()
    {
        Destroy(currentWeapon);
        currentWeapon = Instantiate(inventoryManager.GetItem(selectedWeapon).prefab, weaponHolder);

    }

}
