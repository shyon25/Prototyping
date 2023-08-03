using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleDragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public TileChunkComponents myChunkComponent;

    void Start()
    {
        myChunkComponent = this.GetComponent<TileChunkComponents>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        myChunkComponent.selectMe();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        myChunkComponent.dropMe();
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
