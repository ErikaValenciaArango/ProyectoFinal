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
     private Weapon newWeapon;
     private Consumable newItem;


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
            if (newWeapon != null)
            {
                inventory.AddItem(newWeapon);
                newWeapon = null;
            }
            if (newItem != null)
            {

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
                    if (hit.transform.GetComponent<PickupItem>().item as Weapon)
                    {
                        //Agregar elementos al inventario (CHECKET WRITING BY ANDRES)
                        newWeapon = hit.transform.GetComponent<PickupItem>()?.item as Weapon;

                    }
                    else if (hit.transform.GetComponent<PickupItem>().item as Consumable)
                    {
                        newItem = hit.transform.GetComponent<PickupItem>()?.item as Consumable;
                        if (newItem.type == ConsumableType.Medkit)
                        {
                            //Heal
                        }
                        else if (newItem.type == ConsumableType.Ammo)
                        {
                            //Ammo
                        }
                    }
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
