using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // Reference to the script to highlight the objects 
    protected Outline outline;
    public string message;

    public UnityEvent onInteraction;

    protected virtual void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public virtual void Interact()
    {
        onInteraction.Invoke();
    }

    public void DisableOutline() { outline.enabled = false; }
    public void EnableOutline() { outline.enabled = true; }
}

