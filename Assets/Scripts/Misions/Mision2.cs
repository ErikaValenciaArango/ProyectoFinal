using UnityEngine;
using System.Collections;

public class Mision2 : MonoBehaviour
{
    public GameObject objeto;
    private int _numObj = 0;
    public static bool misionSegunda = false;

    void Update()
    {
        objeto = GameObject.FindWithTag("piedras");
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.tag == "Player")
        {
            Opciones2.piedras += 1;
            DestroyObject(this.gameObject);
        }
    }

    void OnTriggerExit()
    {
        return;
    }
}
