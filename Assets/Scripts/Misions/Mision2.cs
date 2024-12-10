using UnityEngine;
using System.Collections;

public class Mision2 : MonoBehaviour
{
    public GameObject objeto;
    private int _numObj = 0;
    public static bool misionSegunda = false;
    public int layerPiedras; // Variable para almacenar la capa de los objetos que deseas buscar

    void Update()
    {
        // Encuentra el primer objeto en la escena que tenga la capa especificada
        GameObject[] objetos = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objetos)
        {
            if (obj.layer == layerPiedras)
            {
                objeto = obj;
                break;
            }
        }
    }
}
