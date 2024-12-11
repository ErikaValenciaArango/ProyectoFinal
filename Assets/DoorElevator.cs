using System.Collections;
using TMPro;
using UnityEngine;

public class DoorElevator : MonoBehaviour
{
    public bool Open = false;
    public bool HasFuse = false; // Variable para verificar si el fusible ha sido recogida
    public Animator Animator;

    public TextMeshProUGUI textMeshProObject; // Referencia al objeto TextMeshPro


    private void Start()
    {
        Animator = GetComponentInParent<Animator>();
    }

    public void abrir()
    {
        if (HasFuse)
        {
            Open = true;
            Animator.SetBool("Abrir", Open);
        }
        else
        {
            textMeshProObject.text = "Sin energia! Encuentra el fusible";
            // Activar el objeto TextMeshPro
            textMeshProObject.gameObject.SetActive(true);
            // Iniciar la corrutina para desactivar el objeto después de 3 segundos
            StartCoroutine(DesactivarTexto());
        }

    }
    public void Cerrar()
    {
        Open = false;
        Animator.SetBool("Abrir", Open);
    }

    private IEnumerator DesactivarTexto()
    {
        // Esperar 3 segundos
        yield return new WaitForSeconds(3);
        // Desactivar el objeto TextMeshPro
        textMeshProObject.gameObject.SetActive(false);
    }

}
