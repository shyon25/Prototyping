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
    public Vector2 anchorMin;
    public Vector2 anchorMax;
    public string name;
    public Vector2 pos;
    public Color color;

    public List<tile> tiles;

    public tileChunk()
    {
        tiles = new List<tile>();
    }
    
    public void addTile(tile tile)
    {
        tiles.Add(tile);
    }
    public tile getTile(int index)
    {
        return tiles[index];
    }

    public GameObject findMyChunk()
    {
        GameObject myChunk = GameObject.Find("TileChunk" + pos.x + pos.y);
        return myChunk;
    }
}

public class tile
{
    public Vector2 anchorMin;
    public Vector2 anchorMax;
    public string name;
    public Vector2 pos;
    public Color color;
    public int number;
    public bool isDestroyed;

    public tile()
    {
        isDestroyed = false;
    }

}

public class TilesManagement : MonoBehaviour
{
    tile tempTile;
    tileChunk tempChunk;
    GameObject tempObject;

    [SerializeField]
    public tileDatabase wholeTiles;
    public GameObject chunkPrefab;
    public GameObject tileZone;
    public int minNum;
    public int maxNum;
    public GameObject selectingFrame;
    public ScoreManagement scoreManagement;

    void Start()
    {
        wholeTiles = new tileDatabase();
        selectingFrame.SetActive(false);
        tileInitialize(4, 4);
        drawTile(4, 4);
    }

    public void tileInitialize(int row, int column)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                tempChunk = new tileChunk();
                tempChunk.anchorMax = new Vector2((i + 1) * (1f / row), (j + 1) * (1f / row));
                tempChunk.anchorMin = new Vector2(i * (1f / row), j * (1f / row));
                tempChunk.name = "TileChunk" + i + j;
                tempChunk.pos = new Vector2(i, j);
                tempChunk.color = Color.white;

                for (int k = 0; k < 2; k++)
                {
                    for (int l = 0; l < 2; l++)
                    {
                        tempTile = new tile();
                        tempTile.anchorMax = new Vector2((i + 1) * (1f / 2), (j + 1) * (1f / 2));
                        tempTile.anchorMin = new Vector2(i * (1f / 2), j * (1f / 2));
                        tempTile.name = "Tile" + k + l;
                        tempTile.pos = new Vector2(k, l);
                        tempTile.color = randomColor();
                        tempTile.number = Random.Range(minNum, maxNum + 1);
                        tempChunk.addTile(tempTile);
                    }
                }

                if(j == 0)
                {
                    wholeTiles.addChunk_newLine(tempChunk);
                }
                else
                {
                    wholeTiles.addChunk_normal(tempChunk);
                }
            }
        }
    }

    public void destroyTiles(Vector2 chunkPos, Vector2 tilePos)
    {
        tileChunk currentChunk = wholeTiles.chunks[(int)chunkPos.x][(int)chunkPos.y];
        tile currentTile = currentChunk.getTile((int)(tilePos.x * 2 + tilePos.y));
        Color transparentColor = currentTile.color;

        currentTile.isDestroyed = true;
        //currentChunk.findMyChunk().transform.GetChild((int)(tilePos.x * 2 + tilePos.y)).gameObject.SetActive(false);
        transparentColor.a = 0f;
        currentChunk.findMyChunk().transform.GetChild((int)(tilePos.x * 2 + tilePos.y)).gameObject.GetComponent<Image>().color = transparentColor;
        currentChunk.findMyChunk().transform.GetChild((int)(tilePos.x * 2 + tilePos.y)).GetChild(0).GetComponent<TMP_Text>().color = transparentColor;
        currentChunk.findMyChunk().transform.GetChild((int)(tilePos.x * 2 + tilePos.y)).gameObject.GetComponent<TileComponents>().isDestroyed = true;
    }

    public void drawTile(int row, int column)
    {
        for (int j = 0; j < column; j++)
        {
            for (int i = 0; i < row; i++)
            {
                tempObject = Instantiate(chunkPrefab);
                tempObject.transform.SetParent(tileZone.transform);
                tempObject.GetComponent<RectTransform>().anchorMin = wholeTiles.chunks[i][j].anchorMin;
                tempObject.GetComponent<RectTransform>().anchorMax = wholeTiles.chunks[i][j].anchorMax;
                tempObject.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                tempObject.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);

                tempObject.name = wholeTiles.chunks[i][j].name;
                tempObject.GetComponent<TileChunkComponents>().pos = wholeTiles.chunks[i][j].pos;
                tempObject.GetComponent<TileChunkComponents>().color = wholeTiles.chunks[i][j].color;
                tempObject.GetComponent<Image>().color = tempObject.GetComponent<TileChunkComponents>().color;

                initializingInnerTiles(tempObject, i, j);
            } 
        }
    }
    public void initializingInnerTiles(GameObject tileChunk, int row, int column)
    {
        for (int i = 0; i < 4; i++)
        {
            tileChunk.transform.GetChild(i).GetComponent<TileComponents>().number = wholeTiles.chunks[row][column].getTile(i).number;
            tileChunk.transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = wholeTiles.chunks[row][column].getTile(i).number.ToString();
            tileChunk.transform.GetChild(i).GetComponent<TileComponents>().pos = wholeTiles.chunks[row][column].getTile(i).pos;
            tileChunk.transform.GetChild(i).GetComponent<TileComponents>().color = wholeTiles.chunks[row][column].getTile(i).color;
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

    public void activateFrame()
    {
        selectingFrame.SetActive(true);
    }

    public void disactivateFrame()
    {
        selectingFrame.SetActive(false);
    }


    public void resetAllTiles()
    {
        int chunkCount = tileZone.transform.childCount;

        if(scoreManagement.initialResetNumber > 0)
        {
            for (int i = 0; i < chunkCount; i++)
            {
                GameObject.Destroy(tileZone.transform.GetChild(i).gameObject);
            }
            wholeTiles = new tileDatabase();
            tileInitialize(4, 4);
            drawTile(4, 4);
            scoreManagement.useReset();
        }
        
    }

    public void fillTiles()
    {
        if (scoreManagement.initialFillNumber > 0)
        {
            for (int i = 0; i < wholeTiles.chunks.Count; i++)
            {
                for (int j = 0; j < wholeTiles.chunks[i].Count; j++)
                {
                    for (int k = 0; k < wholeTiles.chunks[i][j].tiles.Count; k++)
                    {
                        if (wholeTiles.chunks[i][j].tiles[k].isDestroyed == true)
                        {
                            wholeTiles.chunks[i][j].tiles[k].isDestroyed = false;
                            wholeTiles.chunks[i][j].tiles[k].number = Random.Range(1, 6);
                            wholeTiles.chunks[i][j].tiles[k].color = randomColor();

                            //wholeTiles.chunks[i][j].findMyChunk().transform.GetChild(k).gameObject.SetActive(true);
                            wholeTiles.chunks[i][j].findMyChunk().transform.GetChild(k).GetComponent<TileComponents>().number = wholeTiles.chunks[i][j].tiles[k].number;
                            wholeTiles.chunks[i][j].findMyChunk().transform.GetChild(k).GetChild(0).GetComponent<TMP_Text>().text = wholeTiles.chunks[i][j].tiles[k].number.ToString();
                            wholeTiles.chunks[i][j].findMyChunk().transform.GetChild(k).GetChild(0).GetComponent<TMP_Text>().color = Color.black;
                            wholeTiles.chunks[i][j].findMyChunk().transform.GetChild(k).GetComponent<TileComponents>().color = wholeTiles.chunks[i][j].tiles[k].color;
                            wholeTiles.chunks[i][j].findMyChunk().transform.GetChild(k).GetComponent<Image>().color = wholeTiles.chunks[i][j].tiles[k].color;
                            wholeTiles.chunks[i][j].findMyChunk().transform.GetChild(k).GetComponent<TileComponents>().isDestroyed = false;
                        }
                    }
                }
            }
            scoreManagement.useFill();
        }
    }

}



