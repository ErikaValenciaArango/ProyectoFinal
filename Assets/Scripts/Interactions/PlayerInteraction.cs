using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Distancia para interactuar
    public float playerReach = 2f;
    // Radio de la esfera para la detección
    public float sphereRadius = 0.5f;
    Interactable currentInteractable;

    private InputManager inputManager;

    //llamada a inventario   (CHECKET WRITING BY ANDRES)

    private InventoryManager inventory;
      private Weapon newItem;

    private void Start()
    {
        inputManager = InputManager.Instance;
        //Agregar elementos al inventario (CHECKET WRITING BY ANDRES)

        inventory = GetComponent<InventoryManager>();
    }

    void Update()
    {
        CheckInteraction();
        if (inputManager.PlayerInteracted() && currentInteractable != null)
        {
            currentInteractable.Interact();
            //Agregar elementos al inventario (CHECKET WRITING BY ANDRES)
            if (newItem != null)
            {
                inventory.AddItem(newItem);
            }
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        // Usar SphereCast en lugar de Raycast
        //Se anadio el layer para verificar le funcionamiento
        if (Physics.SphereCast(ray, sphereRadius, out hit, playerReach))
        {

            if (hit.collider.tag == "Interactable") // Si se está mirando un objeto interactuable
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                if (hit.transform.TryGetComponent<PickupItem>(out PickupItem pickupItem))
                {
                    //Agregar elementos al inventario (CHECKET WRITING BY ANDRES)
                    newItem = hit.transform.GetComponent<PickupItem>()?.item as Weapon;

                }


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
