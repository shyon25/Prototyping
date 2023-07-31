using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
   
    public void OnDrop(PointerEventData eventData)
    {
        /*
       DragandDrop item;

       if(this.transform.childCount == 0)
       {
           eventData.pointerDrag.transform.SetParent(this.transform);
       }
       else
       {
           item = eventData.pointerDrag.gameObject.GetComponent<DragandDrop>();
           item.returnToParent();
       }
        */
        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

    }


}
