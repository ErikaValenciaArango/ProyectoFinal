using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    private Text buttonText;

    public Color transparentColor = new Color(1, 1, 1, 0); 
    public Color highlightedColor = new Color(1, 1, 1, 1); 

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<Text>();

        buttonImage.color = transparentColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = transparentColor; 
    }
}
