using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseItem : Interactable
{
    public DoorElevator doorElevator; // Referencia al elevador que se abrirá con este fusible
    public GameObject fakeWall;

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        base.Interact();
        doorElevator.HasFuse = true; // Actualiza el estado del fusible en el elevador
        doorElevator.Animator.SetBool("Energia?", true); // Actualiza el parámetro de animación
        Opciones2.piedras += 1; // Actualiza en QuestGUI2 que el fusible se recogio
        fakeWall.SetActive(false);
        Destroy(gameObject); // Destruye el fusible después de recogerla
    }
}
