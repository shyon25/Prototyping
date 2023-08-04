using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FrameSummoner : MonoBehaviour
{
    public GameObject frame;
    public Canvas canvas;

    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;

    private void Start()
    {
        frame = GameObject.Find("TileSelectFrame").gameObject;
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
    }

    private void Update()
    {
        bool isHitTile = false;
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();

        raycaster.Raycast(pointerEventData, results);

        if(results.Count > 0 )
        {
            for(int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.CompareTag("Tile"))
                {
                    if (frame.GetComponent<FrameComponents>().isActivate)
                    {
                        isHitTile = true;
                        frame.SetActive(true);
                        frame.transform.position = results[i].gameObject.transform.position;
                    }
                    break;
                }
            }
        }
        
        if(!isHitTile)
        {
            frame.SetActive(false);
        }

    }

    /*
    public bool IsPointerOverUI()
    {
        bool result = false;

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, results);

        for(int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.layer == LayerMask.NameToLayer("UI"))
                result = true;
            else
                result = false;
        }


        return result;
    }
    */

    public void moveFrame()
    {

    }
}
