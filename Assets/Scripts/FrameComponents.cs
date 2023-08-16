using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class tileInfo
{
    public int number;
    public Color color;

    public tileInfo(int n, Color c)
    {
        number = n;
        color = c;
    }
}

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
    public ScoreManagement scoreManagement;

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
        //Color tempColor = color;
        Color tempColor = Color.white;

        resetColors();

        //currentColor = tempColor;

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
            
            List<tileInfo> combination = new List<tileInfo>();

            for (int i = 0; i < coloredPoint.Count; i++)
            {
                combination.Add(new tileInfo(sideTile(coloredPoint[i], out error).GetComponent<TileComponents>().number, sideTile(coloredPoint[i], out error).GetComponent<TileComponents>().color));
            }

            if (combo(combination, coloredPoint.Count).Count != 0)
            {
                if (canIRecord())
                {
                    scoreManagement.addBlock(combo(combination, coloredPoint.Count));
                }
                for (int i = 0; i < coloredPoint.Count; i++)
                {
                    Vector2 tempChunkVector = new Vector2(sideTile(coloredPoint[i], out error).transform.parent.GetComponent<TileChunkComponents>().pos.x, sideTile(coloredPoint[i], out error).transform.parent.GetComponent<TileChunkComponents>().pos.y);
                    Vector2 tempTileVector = new Vector2(sideTile(coloredPoint[i], out error).GetComponent<TileComponents>().pos.x, sideTile(coloredPoint[i], out error).GetComponent<TileComponents>().pos.y);
                    tileManagement.destroyTiles(tempChunkVector, tempTileVector);
                }
                
            }
        }
        
    }

    public bool canIRecord()
    {
        bool result = true;
        int error = 0;

        for(int i = 0; i < coloredPoint.Count; i++)
        {
            Vector2 tempChunkVector = new Vector2(sideTile(coloredPoint[i], out error).transform.parent.GetComponent<TileChunkComponents>().pos.x, sideTile(coloredPoint[i], out error).transform.parent.GetComponent<TileChunkComponents>().pos.y);
            Vector2 tempTileVector = new Vector2(sideTile(coloredPoint[i], out error).GetComponent<TileComponents>().pos.x, sideTile(coloredPoint[i], out error).GetComponent<TileComponents>().pos.y);
            if (tileManagement.wholeTiles.chunks[(int)tempChunkVector.x][(int)tempChunkVector.y].tiles[(int)tempTileVector.x * 2 + (int)tempTileVector.y].isDestroyed == true){
                result = result && false;
            }
        }

        return result;
    }

    public List<string> combo(List<tileInfo> combination, int tileNumber)
    {
        List<string> result = new List<string>();

        if (pair(combination, tileNumber))
        {
            switch (tileNumber)
            {
                case 2: result.Add("onepair"); break;
                case 3: result.Add("triple"); break;
                case 4: result.Add("fourcard"); break;
                case 5: result.Add("fivecard"); break;
            }
        }
        if(flush(combination, tileNumber))
        {
            result.Add("flush" + tileNumber.ToString());
        }
        if(straight(combination, tileNumber))
        {
            result.Add("straight" + tileNumber.ToString());
        }
        if (straightFlush(combination, tileNumber))
        {
            result.Add("straightflush" + tileNumber.ToString());
        }            

        return result;
    }

    
    bool pair(List<tileInfo> list, int count)
    {
        bool result = true;

        for(int i = 0; i < count - 1; i++)
        {
            result = result && (list[i].number == list[i + 1].number);
        }

        return result;
    }
    bool flush(List<tileInfo> list, int count)
    {
        bool result = true;

        for (int i = 0; i < count - 1; i++)
        {
            result = result && (list[i].color == list[i + 1].color);
        }

        return result;
    }
    bool straight(List<tileInfo> list, int count)
    {
        List<int> sortedList = new List<int>();
        for(int i = 0; i < list.Count; i++)
        {
            sortedList.Add(list[i].number);
        }
        sortedList.Sort();

        bool result = true;

        for(int i = 0; i < count - 1; i++)
        {
            result = result && (sortedList[i] + 1 == sortedList[i + 1]);
        }

        return result;
    }
    bool straightFlush(List<tileInfo> list, int count)
    {
        return flush(list, count) && straight(list, count);
    }

    /*
    bool twoPair(int n1, int n2, int n3, int n4)
    {
        List<int> sortedList = new List<int>();
        sortedList.Add(n1); sortedList.Add(n2); sortedList.Add(n3); sortedList.Add(n4);
        sortedList.Sort();

        if (sortedList[0] == sortedList[1] && sortedList[2] == sortedList[3])
            return true;
        else
            return false;
    }

    bool fullHouse(List<tileInfo> list)
    {
        List<tileInfo> sortedList = new List<tileInfo>();
        for (int i = 0; i < list.Count; i++)
        {
            sortedList.Add(list[i]);
        }
        sortedList = sortedList.OrderBy(p => p.number).ToList();

        if (triple(sortedList[0].number, sortedList[1].number, sortedList[2].number) && onePair(sortedList[3].number, sortedList[4].number)){
            return true;
        }
        else if (triple(sortedList[2].number, sortedList[3].number, sortedList[4].number) && onePair(sortedList[0].number, sortedList[1].number) )
        {
            return true;
        }
        else
            return false;
    }
    */
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
