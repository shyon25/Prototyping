using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragandDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    RectTransform rectTrans;
    RectTransform parentRectTrans;
    Canvas rootCanvas;
    Vector2 offset;

    public void Start()
    {
        rectTrans = this.GetComponent<RectTransform>();
        parentRectTrans = this.rectTrans.parent as RectTransform;
        this.rootCanvas = this.GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.transform.SetParent(rootCanvas.transform);

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(this.parentRectTrans, eventData.position, (this.rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : this.rootCanvas.worldCamera, out offset))
        {
            this.offset.x = this.offset.x - this.transform.localPosition.x;
            this.offset.y = this.offset.y - this.transform.localPosition.y;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;

        Vector2 OutLocalPos = Vector2.zero;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(this.parentRectTrans, eventData.position, (this.rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : this.rootCanvas.worldCamera, out OutLocalPos))
        {
            this.transform.localPosition = OutLocalPos - offset;
        }
    }

    public void returnToParent()
    {
        this.transform.SetParent(this.parentRectTrans);
    }
}
