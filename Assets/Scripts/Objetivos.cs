using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Objetivos : MonoBehaviour
{
    public int numDeObjetivos;
    public TextMeshProUGUI textoMision;
    public GameObject botonDeMision;
    // Start is called before the first frame update
    void Start()
    {
        numDeObjetivos = GameObject.FindGameObjectsWithTag("objetivo").Length;
        textoMision.text = "Obt�n los objetos" +
                         "\n Restantes: " + numDeObjetivos;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "objetivo")
        {
            Destroy(col.transform.parent.gameObject);
            numDeObjetivos--;
            textoMision.text = "Obt�n los objetos" +
                             "\n Restantes: " + numDeObjetivos;
            if (numDeObjetivos <= 0)
            {
                textoMision.text = "Completaste la misi�n";
                botonDeMision.SetActive(true);
            }
        }
    }
}
