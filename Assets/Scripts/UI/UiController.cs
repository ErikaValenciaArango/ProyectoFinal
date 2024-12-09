using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{

    [SerializeField] GameObject UIWeapon;
    // Start is called before the first frame update
    void Start()
    {
        UIWeapon = GameObject.FindGameObjectWithTag("UIWeapon");
    }

    private void OnEnable() 
    {
        ActiveUIWeapon();
    }

    private void OnDisable() 
    {
        DesactiveUIWeapon();
    }
    public void ActiveUIWeapon()
    {
        UIWeapon.SetActive(true);
    }
        public void DesactiveUIWeapon()
    {
        UIWeapon.SetActive(false);
    }
}
