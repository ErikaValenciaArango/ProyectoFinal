using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerInteraction : MonoBehaviour
{
    // Distancia para interactuar
    public float playerReach = 2f;
    // Radio de la esfera para la detección
    public float sphereRadius = 0.5f;
    Interactable currentInteractable;

    void Update()
    {
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        // Usar SphereCast en lugar de Raycast
        if (Physics.SphereCast(ray, sphereRadius, out hit, playerReach))
        {
            if (hit.collider.tag == "Interactable") // Si se está mirando un objeto interactuable
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                // Si hay un currentInteractable y no es el newInteractable
                if (currentInteractable && newInteractable != currentInteractable)
                {
                    DisableCurrentInteractable();
                }

                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else // Si el nuevo interactuable no está habilitado
                {
                    DisableCurrentInteractable();
                }
            }
            else // Si no es un interactuable
            {
                DisableCurrentInteractable();
            }
        }
        else // Si no hay nada al alcance
        {
            DisableCurrentInteractable();
        }
    }



    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        InteractHUDController.Instance.EnableInteractionText(currentInteractable.message);
    }

    void DisableCurrentInteractable()
    {
        InteractHUDController.Instance.DisableInteractionText();
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}