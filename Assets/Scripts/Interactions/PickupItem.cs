using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PickupItem : Interactable
{
    public Weapon item;
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
