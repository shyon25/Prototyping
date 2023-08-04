using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleDragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler
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

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.gameObject.GetComponent<TileChunkComponents>().switchChunk(this.gameObject);
    }
}
