using TMPro; // Importar el namespace de TextMeshPro

public class ElevatorInteraction : Interactable
{
    public TextMeshProUGUI textMeshProObject; // Referencia al objeto TextMeshPro
    public DoorElevator doorElevator; // Referencia a la instancia de DoorElevator

    protected override void Start()
    {
        base.Start();
        // Inicialización específica para ActivateObjectsMoving
    }

    public override void Interact()
    {
        base.Interact();
        // Verificar si HasFuse es false
        /*
        if (!doorElevator.HasFuse)
        {
            // Activar el objeto TextMeshPro
            textMeshProObject.gameObject.SetActive(true);
        }
        else
        {
            // Implementación específica para activar el botón
            doorElevator.abrir();
*/
        // Asegurarse de que el objeto no se desactive
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        //}
    }
}
