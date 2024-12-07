using UnityEngine;


[CreateAssetMenu (fileName = "new Weapon", menuName = "Items/Weapon")]

public class Weapon : Item
{
    public GameObject prefab; // prefab del arma
    public int magazineSize; // tamano del cargador de balas
    public int magazineCount; // cantidad de cargadores que tiene
    public float range;// rango de disparo
    public WeaponType weaponType; // tipo de arma
    public WeaponStyle weaponStyle; // slot donde esta el arma
}

public enum WeaponType
{
    Pistol,Melee,Hands
}

public enum WeaponStyle
{
  Primary,Melee
}
