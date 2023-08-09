using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FrameComponents : MonoBehaviour
{
    public float selectingColor;
    public float nonSelectingColor;
    public bool isActivate;
    public int currentStampNumber;
    public List<Vector2> coloredPoint;
    public Vector2 currentTilePos;
    public Vector2 currentChunkPos;
    public Color currentColor;
    public TilesManagement tileManagement;

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
        currentTilePos = new Vector2();
        currentChunkPos = new Vector2();
        currentColor = Color.white;
    }

    public void changeColors(List<Vector2> points, Color color)
    {
        GameObject currentTile;
        Color tempColor = color;

        resetColors();

        currentColor = tempColor;

        coloredPoint = points;

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

    public void attackTile()
    {
        int error = 0;
        if(coloredPoint.Count > 0)
        {
            for(int i = 0; i < coloredPoint.Count; i++)
            {
                sideTile(coloredPoint[i], out error); 
                if (error == -1)
                {
                    Debug.Log("I will not attack");
                    return;
                }
            }
            
            List<int> combination = new List<int>();

            for (int i = 0; i < coloredPoint.Count; i++)
            {
                if (sideTile(coloredPoint[i], out error).GetComponent<TileComponents>().color == currentColor)
                {
                    combination.Add(sideTile(coloredPoint[i], out error).GetComponent<TileComponents>().number);
                }
            }

            if (combo(combination))
            {
                for (int i = 0; i < coloredPoint.Count; i++)
                {
                    Vector2 tempChunkVector = new Vector2(sideTile(coloredPoint[i], out error).transform.parent.GetComponent<TileChunkComponents>().pos.x, sideTile(coloredPoint[i], out error).transform.parent.GetComponent<TileChunkComponents>().pos.y);
                    Vector2 tempTileVector = new Vector2(sideTile(coloredPoint[i], out error).GetComponent<TileComponents>().pos.x, sideTile(coloredPoint[i], out error).GetComponent<TileComponents>().pos.y);
                    tileManagement.destoryTile(tempChunkVector, tempTileVector);
                }
            }
        }
        
    }

    public bool combo(List<int> combination)
    {
        bool isExist = true;
        int firstNumber = 0;

        if(combination.Count > 0)
        {
            firstNumber = combination[0];
        }
        else
        {
            isExist = false;
        }

        for(int i = 0; i < combination.Count; i++)
        { 
            if(firstNumber != combination[i])
            {
                isExist = false;
            }
        }

        return isExist;
    }

    public GameObject sideTile(Vector2 frameTilePos, out int error)
    {
        error = 0;
        whereIsTheTile currentPos = new whereIsTheTile(currentChunkPos, currentTilePos);

        if(frameTilePos.x == 0)
        {
            error = currentPos.goLeft();
        }
        if(frameTilePos.y == 0)
        {
            error = currentPos.goDown();
        }
        if(frameTilePos.x == 2)
        {
            error = currentPos.goRight();
        }
        if(frameTilePos.y == 2)
        {
            error = currentPos.goUp();
        }
        
        GameObject nowChunk = tileManagement.wholeTiles.chunks[(int)currentPos.chunkPos.x][(int)currentPos.chunkPos.y].findMyChunk();
        GameObject nowTile = nowChunk.transform.GetChild((int)(currentPos.tilePos.x * 2 + currentPos.tilePos.y)).gameObject;

        return nowTile;
    }


}

class whereIsTheTile
{
    public Vector2 chunkPos;
    public Vector2 tilePos;

    public whereIsTheTile(Vector2 currentChunkPos, Vector2 currentTilePos)
    {
        chunkPos = currentChunkPos;
        tilePos = currentTilePos;
    }
    public int goLeft()
    {
        int error = 0;
        
        if(tilePos.x == 0)
        {
            if (chunkPos.x == 0)
            {
                error = -1;
            }
            else
            {
                chunkPos.x -= 1;
                tilePos.x += 1;
                error = 0;
            }
        }
        else
        {
            tilePos.x -= 1;
            error = 0;
        }
              
        return error;
    }

    public int goRight()
    {
        int error = 0;

        if (tilePos.x == 1)
        {
            if (chunkPos.x == 3)
            {
                error = -1;
            }
            else
            {
                chunkPos.x += 1;
                tilePos.x -= 1;
                error = 0;
            }
        }
        else
        {
            tilePos.x += 1;
            error = 0;
        }

        return error;
    }

    public int goUp()
    {
        int error = 0;

        if (tilePos.y == 1)
        {
            if (chunkPos.y == 3)
            {
                error = -1;
            }
            else
            {
                chunkPos.y += 1;
                tilePos.y -= 1;
                error = 0;
            }
        }
        else
        {
            tilePos.y += 1;
            error = 0;
        }

        return error;
    }

    public int goDown()
    {
        int error = 0;

        if (tilePos.y == 0)
        {
            if (chunkPos.y == 0)
            {
                error = -1;
            }
            else
            {
                chunkPos.y -= 1;
                tilePos.y += 1;
                error = 0;
            }
        }
        else
        {
            tilePos.y -= 1;
            error = 0;
        }

        return error;
    }
}
