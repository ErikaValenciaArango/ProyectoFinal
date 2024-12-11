using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorElevator : MonoBehaviour
{
    public bool Open = false;
    public bool HasFuse = false; // Variable para verificar si el fusible ha sido recogida
    public Animator Animator;

    public TextMeshProUGUI textMeshProObject; // Referencia al objeto TextMeshPro
    [SerializeField]private  GameObject finalScreen;


    private void Start()
    {
        Animator = GetComponentInParent<Animator>();
        finalScreen.SetActive(false);
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
        if (finalScreen == null )
        {
            Debug.LogWarning("No se cuenta con el objeto final scree");
        } 
        Open = false;
        Animator.SetBool("Abrir", Open);
        StartCoroutine (FinalScreenTimeLaps());
    }

    private IEnumerator DesactivarTexto()
    {
        // Esperar 3 segundos
        yield return new WaitForSeconds(3);
        // Desactivar el objeto TextMeshPro
        textMeshProObject.gameObject.SetActive(false);
    }

    private IEnumerator FinalScreenTimeLaps()
    {
        yield return new WaitForSeconds(3);
        finalScreen.SetActive(true);
        // Esperar 3 segundos
        yield return new WaitForSeconds(5);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        yield return new WaitForSeconds(1);
        finalScreen.SetActive(false);
    }

}
