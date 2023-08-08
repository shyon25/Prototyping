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
        moving();

        if(Input.GetMouseButtonDown(0))
        {
            clicking();
        }
    }

    public void moving()
    {
        bool isHitTile = false;
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();

        raycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.CompareTag("Tile"))
                {
                    if (frame.GetComponent<FrameComponents>().isActivate)
                    {
                        isHitTile = true;
                        frame.SetActive(true);
                        frame.transform.position = results[i].gameObject.transform.position;
                        frame.GetComponent<FrameComponents>().currentChunkPos = results[i].gameObject.transform.parent.GetComponent<TileChunkComponents>().pos;
                        frame.GetComponent<FrameComponents>().currentTilePos = results[i].gameObject.GetComponent<TileComponents>().pos;
                    }
                    break;
                }
            }
        }

        if (!isHitTile)
        {
            frame.SetActive(false);
        }
    }

    public void clicking()
    {
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();

        raycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.CompareTag("Tile"))
                {
                    if (frame.GetComponent<FrameComponents>().isActivate)
                    {
                        frame.GetComponent<FrameComponents>().attackTile();
                    }
                    break;
                }
            }
        }
    }
}
