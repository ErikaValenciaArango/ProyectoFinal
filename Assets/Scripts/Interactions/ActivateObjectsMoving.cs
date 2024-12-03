using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class ActivateObjectsMoving : Interactable
{
    public Library library;

    protected override void Start()
    {
        base.Start();
        // Inicialización específica para ActivateObjectsMoving
    }

    public override void Interact()
    {
        base.Interact();
        // Implementación específica para activar la palanca
        library.Open();

        // Asegurarse de que el objeto no se desactive
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }
}

