using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f);
    public float scaleSpeed = 10f;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isHovered = false;

    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = hoverScale;
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
        isHovered = false;
    }
}
