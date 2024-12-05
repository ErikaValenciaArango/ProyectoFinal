using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    private Text buttonText;

    public Color normalColor = new Color(1, 1, 1, 0); 
    public Color highlightedColor = new Color(1, 1, 1, 1); 
    public Color pressedColor = new Color(0.8f, 0.8f, 0.8f, 1); 

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<Text>();

        ResetButtonState();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = highlightedColor; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = normalColor; 
    }

    public void ResetButtonState()
    {
        buttonImage.color = normalColor;
    }

    public void OnButtonPressed()
    {
        buttonImage.color = pressedColor; 
    }
}

