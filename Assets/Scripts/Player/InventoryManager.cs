using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private Weapon[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();    
    }

    private void InitVariables()
    {
        weapons = new Weapon[2];
    }

    public void AddItem(Weapon item)
    {
        int newItemIndex = (int)item.weaponStyle;

        if (weapons[newItemIndex] != null)
        {
            RemoveItem(newItemIndex);
        }
        weapons[newItemIndex] = item;

    }

    //Revision este metodo depronto no es necesario ya qu eno puede encontrar mas armas iguales pero por posibles bugs futuros con el spawn
    public void RemoveItem(int index)
    {
        weapons[index] = null;
    }

    public Weapon GetItem(int index)
    {
        return weapons[index];
    }

}
