using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public AudioSource audioSource;
    public AudioClip swap;
    public AudioClip click;

    private void Start()
    {
        selected = false;
        tileManager = GameObject.Find("TileZone").GetComponent<TilesManagement>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void playSwap()
    {
        audioSource.clip = swap;
        audioSource.Play();
    }
    void playClick()
    {
        audioSource.clip = click;
        audioSource.Play();
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
        Color transparent = Color.black;
        transparent.a = 0f;

        myChunkObject = myChunk.findMyChunk();

        for(int i = 0; i < newTiles.Count; i++)
        {
            myChunkObject.transform.GetChild(i).GetComponent<TileComponents>().number = newTiles[i].number;
            myChunkObject.transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = newTiles[i].number.ToString();
            myChunkObject.transform.GetChild(i).GetComponent<TileComponents>().color = newTiles[i].color;
            myChunkObject.transform.GetChild(i).GetComponent<Image>().color = newTiles[i].color;
            myChunkObject.transform.GetChild(i).GetComponent<TileComponents>().isDestroyed = newTiles[i].isDestroyed;
            if (myChunk.tiles[i].isDestroyed == false)
            {
                //myChunkObject.transform.GetChild(i).gameObject.SetActive(true);
                myChunkObject.transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().color = Color.black;
                myChunkObject.transform.GetChild(i).GetComponent<Image>().color = myChunkObject.transform.GetChild(i).GetComponent<TileComponents>().color;
            }
            else
            {
                //myChunkObject.transform.GetChild(i).gameObject.SetActive(false);
                myChunkObject.transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().color = transparent;
                myChunkObject.transform.GetChild(i).GetComponent<Image>().color = transparent;
            }
        }

    }
    public void selectMe()
    {
        if(selected == false)
        {
            selected = true;
            this.GetComponent<RectTransform>().offsetMax = new Vector2(-10, -10);
            this.GetComponent<RectTransform>().offsetMin = new Vector2(10, 10);
            playClick();
        }
        
    }

    public void dropMe()
    {
        if (selected == true)
        {
            selected = false;
            this.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            this.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            playSwap();
        }
    }

    public void switchChunk(GameObject otherChunk)
    {
        tileChunk yourChunk;
        Vector2 yourPos = otherChunk.GetComponent<TileChunkComponents>().pos;

        myChunk = tileManager.wholeTiles.chunks[(int)pos.x][(int)pos.y];
        yourChunk = tileManager.wholeTiles.chunks[(int)yourPos.x][(int)yourPos.y];
        /*
        tileManager.wholeTiles.chunks[(int)pos.x][(int)pos.y] = yourChunk;
        tileManager.wholeTiles.chunks[(int)yourPos.x][(int)yourPos.y] = myChunk;
        */
        List <tile> tempTile = myChunk.tiles;
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

        Vector2 tempPos = this.GetComponent<TileChunkComponents>().pos;
        this.GetComponent<TileChunkComponents>().pos = otherChunk.GetComponent<TileChunkComponents>().pos;
        otherChunk.GetComponent<TileChunkComponents>().pos = tempPos;

    }
}
