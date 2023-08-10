using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour
{
    public GameObject resetButton;
    public int initialResetNumber;
    public TMP_Text scoreText;

    List<earnedBlock> earnedBlocks;

    private void Start()
    {
        resetButton.transform.GetChild(1).GetComponent<TMP_Text>().text = initialResetNumber.ToString();
        earnedBlocks = new List<earnedBlock>();
        scoreText = this.gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        scoreText.text = "";
    }

    public void useReset()
    {
        if(initialResetNumber > 0)
        {
            initialResetNumber -= 1;
            resetButton.transform.GetChild(1).GetComponent<TMP_Text>().text = initialResetNumber.ToString();
        }
    }

    public void addBlock(Color color, int number, int count)
    {
        earnedBlocks.Add(new earnedBlock(color, number, count));
        recordBlock();
    }

    void recordBlock()
    {
        scoreText.text = "";
        if(earnedBlocks.Count > 0)
        {
            for(int i = 0; i < earnedBlocks.Count; i++)
            {
                scoreText.text += earnedBlocks[i].count.ToString() + namedColor(earnedBlocks[i].color) + earnedBlocks[i].number.ToString() + " ";
            }
        }
    }

    string namedColor(Color color)
    {
        string result = "";

        if(color == Color.red)
        {
            result = "R";
        }
        else if(color == Color.green)
        {
            result = "G";
        }
        else if(color == Color.blue)
        {
            result = "B";
        }
        else if(color == Color.yellow)
        {
            result = "Y";
        }
        else if(color == Color.white)
        {
            result = "W";
        }

        return result;
    }

}

public class earnedBlock
{
    public Color color;
    public int number;
    public int count;

    public earnedBlock(Color c, int n, int t)
    {
        color = c;
        number = n;
        count = t;
    }
}
