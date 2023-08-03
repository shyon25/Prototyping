using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileChunkComponents : MonoBehaviour
{
    TilesManagement tileManager;
    tileChunk myChunk;
    GameObject myChunkObject;
    List<tile> newTiles;
    bool selected;

    [SerializeField]
    public Vector2 pos;
    public Color color;

    private void Start()
    {
        selected = false;
        tileManager = GameObject.Find("TileZone").GetComponent<TilesManagement>();
    }

    public void rotateTiles()
    {
        myChunk = tileManager.wholeTiles.chunks[(int)pos.x][(int)pos.y];

        newTiles = new List<tile>();
        newTiles.Add(myChunk.tiles[2]);
        newTiles.Add(myChunk.tiles[0]);
        newTiles.Add(myChunk.tiles[3]);
        newTiles.Add(myChunk.tiles[1]);
        myChunk.tiles = newTiles;

        myChunkObject = myChunk.findMyChunk();

        for(int i = 0; i < newTiles.Count; i++)
        {
            myChunkObject.transform.GetChild(i).GetComponent<TileComponents>().number = newTiles[i].number;
            myChunkObject.transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = newTiles[i].number.ToString();
            myChunkObject.transform.GetChild(i).GetComponent<TileComponents>().color = newTiles[i].color;
            myChunkObject.transform.GetChild(i).GetComponent<Image>().color = newTiles[i].color;
        }

    }
    public void selectMe()
    {
        Debug.Log("selected " + selected + " select Me");
        if(selected == false)
        {
            selected = true;
            this.GetComponent<RectTransform>().offsetMax = new Vector2(-10, -10);
            this.GetComponent<RectTransform>().offsetMin = new Vector2(10, 10);
        }
        
    }

    public void dropMe()
    {
        Debug.Log("selected " + selected + " drop Me");
        if (selected == true)
        {
            selected = false;
            this.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            this.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        }
    }

    public void switchChunk(GameObject otherChunk)
    {
        tileChunk yourChunk;
        Vector2 yourPos = otherChunk.GetComponent<TileChunkComponents>().pos;

        myChunk = tileManager.wholeTiles.chunks[(int)pos.x][(int)pos.y];
        yourChunk = tileManager.wholeTiles.chunks[(int)yourPos.x][(int)yourPos.y];

        List<tile> tempTile = myChunk.tiles;
        myChunk.tiles = yourChunk.tiles;
        yourChunk.tiles = tempTile;

        string tempName = this.gameObject.name;
        this.gameObject.name = otherChunk.gameObject.name;
        otherChunk.gameObject.name = tempName;

        Vector2 tempAnchorMax = this.GetComponent<RectTransform>().anchorMax;
        Vector2 tempAnchorMin = this.GetComponent<RectTransform>().anchorMin;
        this.GetComponent<RectTransform>().anchorMax = otherChunk.GetComponent<RectTransform>().anchorMax;
        this.GetComponent<RectTransform>().anchorMin = otherChunk.GetComponent<RectTransform>().anchorMin;
        otherChunk.GetComponent<RectTransform>().anchorMax = tempAnchorMax;
        otherChunk.GetComponent<RectTransform>().anchorMin = tempAnchorMin;


    }
}
