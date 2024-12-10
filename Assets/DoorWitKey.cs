using UnityEngine;

public class DoorWitKey : MonoBehaviour
{
    public bool Open = false;
    public bool HasKey = false; // Variable para verificar si la llave ha sido recogida
    public Animator Animator;

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

    }
}
