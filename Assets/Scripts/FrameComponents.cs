using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameComponents : MonoBehaviour
{
    public float selectingColor;
    public float nonSelectingColor;
    public bool isActivate;
    public int currentStampNumber;
    public List<Vector2> coloredPoint;

    List<List<GameObject>> frameTiles;
    private void Start()
    {
        frameTiles = new List<List<GameObject>>();

        for(int i = 0; i < 3; i++)
        {
            frameTiles.Add(new List<GameObject>());
            for (int j = 0; j < 3; j++)
            {
                frameTiles[i].Add(this.transform.GetChild(i * 3 + j).gameObject);
            }
        }

        isActivate = false;
        currentStampNumber = 0;
        coloredPoint = new List<Vector2>();
    }

    public void changeColors(List<Vector2> points, Color color)
    {
        GameObject currentTile;
        Color tempColor = color;

        resetColors();

        for(int i = 0; i < points.Count;i++)
        {
            tempColor.a = selectingColor;
            currentTile = frameTiles[(int)points[i].x][(int)points[i].y];
            currentTile.GetComponent<Image>().color = tempColor;
        }
    }

    public void resetColors()
    {
        Color tempColor;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tempColor = Color.white;
                tempColor.a = nonSelectingColor;
                frameTiles[i][j].GetComponent<Image>().color = tempColor;
            }
        }
    }


}
