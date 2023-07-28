using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TilesManagement : MonoBehaviour
{
    [SerializeField]
    public GameObject tilePrefab;
    public GameObject tempTile;
    public int minNum;
    public int maxNum;

    void Start()
    {
        generateTile(8, 8);
    }
    public void generateTile(int row, int column)
    {
        for (int j = 0; j < column; j++)
        {
            for (int i = 0; i < row; i++)
            {
                tempTile = Instantiate(tilePrefab, this.transform);
                tempTile.GetComponent<RectTransform>().anchorMin = new Vector2(i * (1f/row), j * (1f/column));
                tempTile.GetComponent<RectTransform>().anchorMax = new Vector2((i + 1) * (1f / row), (j + 1) * (1f / row));
                tempTile.name = "Tile" + j + i;
                tempTile.GetComponent<TileComponents>().pos = new Vector2(i, j);
                tempTile.GetComponent<TileComponents>().number = Random.Range(minNum, maxNum + 1);
                tempTile.transform.GetChild(0).GetComponent<TMP_Text>().text = tempTile.GetComponent<TileComponents>().number.ToString();
                tempTile.GetComponent<TileComponents>().color = randomColor();
                tempTile.GetComponent<Image>().color = tempTile.GetComponent<TileComponents>().color;
            }
        }
        

    }

    public Color randomColor()
    {
        Color resultColor = new Color();
        int randInt = Random.Range(1, 6);
        switch (randInt)
        {
            case 1: resultColor = Color.red;
                break;
            case 2: resultColor = Color.green;
                break;
            case 3:resultColor = Color.blue;
                break;
            case 4:resultColor = Color.white;
                break;
            case 5:resultColor = Color.yellow;
                break;
            default:
                break;
        }
        return resultColor;
    }

}

