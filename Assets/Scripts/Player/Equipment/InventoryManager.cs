using System;
using System.Reflection;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    private PlayerHUD playerHUD;

    public WeaponSwitching activeWeapon;
    //validar el tamano dle inventario
    public int InventorySize => weapons.Length;

    void Start()
    {
        GetReferences();
        InitVariables();
    }


    private void InitVariables()
    {

        weapons = new Weapon[2]; // Inicializaci�n de armas, ajusta seg�n sea necesario
        activeWeapon = GetComponent<WeaponSwitching>();
    }

    public void AddItem(Weapon item)
    {

        int newItemIndex = (int)item.weaponStyle;




        if (weapons[newItemIndex] != null)
        {
            RemoveItem(newItemIndex);
        }

        weapons[newItemIndex] = item;

        activeWeapon.UnequipWeapon();
        activeWeapon.EquipWeapon(item);

        // Actualizar UI del arma (puedes cambiar seg�n tu implementaci�n de UI)
        playerHUD.UpdateWeaponUI(item);
    }

    public void RemoveItem(int index)
    {
        weapons[index] = null;
    }

    public Weapon GetItem(int index)
    {

        return weapons[index];
    }

    private void GetReferences()
    {
        playerHUD = GetComponent<PlayerHUD>();
    }
}
