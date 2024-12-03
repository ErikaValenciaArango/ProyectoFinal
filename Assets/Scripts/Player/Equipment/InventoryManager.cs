using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    private PlayerHUD playerHUD;

    public event Action<Weapon> OnWeaponAdded; // Evento para notificar cuando se agrega un arma

    void Start()
    {
        GetReferences();
        InitVariables();
    }

    private void InitVariables()
    {
        weapons = new Weapon[2]; // Inicializaci�n de armas, ajusta seg�n sea necesario
    }

    public void AddItem(Weapon item)
    {
        int newItemIndex = (int)item.weaponStyle;

            if (weapons[newItemIndex] != null)
            {
                RemoveItem(newItemIndex);
            }

        weapons[newItemIndex] = item;

        // Disparar evento para notificar que un arma ha sido a�adida
        OnWeaponAdded?.Invoke(item);

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
