using System.Collections;
using TMPro;
using UnityEngine;

public class DoorWitKey : MonoBehaviour
{
    public bool Open = false;
    public bool HasKey = false; // Variable para verificar si la llave ha sido recogida
    public Animator Animator;

    public TextMeshProUGUI textMeshProObject; // Referencia al objeto TextMeshPro


    private void Start()
    {
        Animator = GetComponentInParent<Animator>();
    }

    public void CambiarValor()
    {
        if (HasKey) // Verificar si la llave ha sido recogida
        {
            Open = !Open;
            Animator.SetTrigger("Interactuo?");
            Animator.SetBool("Abrir", Open);
        }
        else
        {
            textMeshProObject.text = "Necesitas una llave para abrir la puerta";
            // Activar el objeto TextMeshPro
            textMeshProObject.gameObject.SetActive(true);
            // Iniciar la corrutina para desactivar el objeto después de 3 segundos
            StartCoroutine(DesactivarTexto());
        }

    }

    private IEnumerator DesactivarTexto()
    {
        // Esperar 3 segundos
        yield return new WaitForSeconds(3);
        // Desactivar el objeto TextMeshPro
        textMeshProObject.gameObject.SetActive(false);
    }
}
