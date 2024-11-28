using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractHUDController : MonoBehaviour
{
    public static InteractHUDController Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] TMP_Text interactionText;

    public void EnableInteractionText(string text)
    {
        interactionText.text = text + " (F)";
        interactionText.gameObject.SetActive(true);
    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }
}
