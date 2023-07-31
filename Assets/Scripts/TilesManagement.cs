using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class tileDatabase
{
    public List<List<tileChunk>> chunks;
    public int currentLine;

    public tileDatabase()
    {
        chunks = new List<List<tileChunk>>();
        currentLine = -1;
    }
    public void addChunk_normal(tileChunk chunk)
    {
        chunks[currentLine].Add(chunk);
    }
    public void addChunk_newLine(tileChunk chunk)
    {
        currentLine += 1;
        chunks.Add(new List<tileChunk>());
        chunks[currentLine].Add(chunk);
    }
    public tileChunk getTileChunk(int row, int column)
    {
        return chunks[row][column];
    }
}

public class tileChunk
{
    public List<GameObject> tiles;
    
    public void addTile(GameObject tile)
    {
        tiles.Add(tile);
    }
    public GameObject getTile(int index)
    {
        return tiles[index];
    }
}

public class TilesManagement : MonoBehaviour
{
    GameObject tempTile;
    tileChunk tempChunk;

    [SerializeField]
    public tileDatabase wholeTiles;
    public GameObject tilePrefab;
    public int minNum;
    public int maxNum;

    void Start()
    {
        wholeTiles = new tileDatabase();
        generateTile(4, 4);
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
                tempTile.name = "TileChunk" + j + i;
                tempTile.GetComponent<TileChunkComponents>().pos = new Vector2(i, j);
                tempTile.GetComponent<TileChunkComponents>().color = Color.gray;
                tempTile.GetComponent<Image>().color = tempTile.GetComponent<TileChunkComponents>().color;

                initializingInnerTiles(tempTile);
                
            }
        }
    }
    public void initializingInnerTiles(GameObject tileChunk)
    {
        for(int i = 0; i < 4; i++)
        {
            tileChunk.transform.GetChild(i).GetComponent<TileComponents>().number = Random.Range(minNum, maxNum + 1);
            tileChunk.transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = tileChunk.transform.GetChild(i).GetComponent<TileComponents>().number.ToString();
            tileChunk.transform.GetChild(i).GetComponent<TileComponents>().color = randomColor();
            tileChunk.transform.GetChild(i).GetComponent<Image>().color = tileChunk.transform.GetChild(i).GetComponent<TileComponents>().color;
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



