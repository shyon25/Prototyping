using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChunkComponents : MonoBehaviour
{
    [SerializeField]
    public Vector2 pos;
    public Color color;

    public void rotateTiles()
    {
        Vector2 tempMax = new Vector2(0, 0);
        Vector2 tempMin = new Vector2(0, 0);
        Vector2 tempPos = new Vector2(0, 0);

        tempMax = this.transform.GetChild(0).GetComponent<RectTransform>().anchorMax;
        tempMin = this.transform.GetChild(0).GetComponent<RectTransform>().anchorMin;
        tempPos = this.transform.GetChild(0).GetComponent<TileComponents>().pos;

        moveTile(1, 0);
        moveTile(3, 1);
        moveTile(2, 3);
       
        this.transform.GetChild(2).GetComponent<RectTransform>().anchorMax = tempMax;
        this.transform.GetChild(2).GetComponent<RectTransform>().anchorMin = tempMin;
        this.transform.GetChild(2).GetComponent<TileComponents>().pos = tempPos;

    }

    public void moveTile(int depart, int arrive)
    {
        this.transform.GetChild(arrive).GetComponent<RectTransform>().anchorMax = this.transform.GetChild(depart).GetComponent<RectTransform>().anchorMax;
        this.transform.GetChild(arrive).GetComponent<RectTransform>().anchorMin = this.transform.GetChild(depart).GetComponent<RectTransform>().anchorMin;
        this.transform.GetChild(arrive).GetComponent<TileComponents>().pos = this.transform.GetChild(depart).GetComponent<TileComponents>().pos;
    }
}
