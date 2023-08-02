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

    [SerializeField]
    public Vector2 pos;
    public Color color;

    private void Start()
    {
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
}
