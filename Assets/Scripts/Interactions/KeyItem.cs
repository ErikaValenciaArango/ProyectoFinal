using UnityEngine;

public class KeyItem : Interactable
{
    public DoorWitKey door; // Referencia a la puerta que se abrirá con esta llave

    protected override void Start()
    {
        base.Start();
        // Inicialización específica para KeyItem
    }

    public override void Interact()
    {
        base.Interact();
        door.HasKey = true; // Actualiza el estado de la llave en la puerta
        door.Animator.SetBool("LLave?", true); // Actualiza el parámetro de animación
        Opciones2.piedras += 1; // Actualiza en QuestGUI2 que la llave se recogio
        Destroy(gameObject); // Destruye la llave después de recogerla
    }
}