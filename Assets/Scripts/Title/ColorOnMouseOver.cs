using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeColorOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MeshRenderer model; // 1
    public Color normalColor; // 2
    public Color hoverColor; // 3

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       model.material.color = normalColor; 
    }
    public void OnPointerEnter(PointerEventData eventData) // 1
    {
        model.material.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) // 2
    {
        model.material.color = normalColor;
    }

}