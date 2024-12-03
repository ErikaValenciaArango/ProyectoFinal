using UnityEngine;

public class PickupItem : Interactable
{
    protected override void Start()
    {
        base.Start();
        // Inicialización específica para PickupItem
    }

    public override void Interact()
    {
        base.Interact();
        // Implementación específica para recoger el objeto
        Destroy(gameObject);
    }
}

