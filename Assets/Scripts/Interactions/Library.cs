using UnityEngine;

public class Library : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void Open()
    {
        isOpen = !isOpen; // Cambia el estado de la puerta
        animator.SetBool("IsOpen", isOpen); // Actualiza el parámetro de la animación
        Opciones2.piedras += 1; // Actualiza en QuestGUI2 que la palanca se encontro

    }
}
