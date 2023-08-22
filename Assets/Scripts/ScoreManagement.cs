using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour
{
    public List<int> goalCounts;

    public GameObject resetButton;
    public int initialResetNumber;
    public GameObject fillButton;
    public int initialFillNumber;
    public TMP_Text scoreText;
    public TMP_Text goalText;
    public LoadClearScene loadClearScene;

    public List<string> earnedText;
    public List<string> goals;
    public List<int> completeCounter;

    private void Start()
    {
        resetButton.transform.GetChild(1).GetComponent<TMP_Text>().text = initialResetNumber.ToString();
        fillButton.transform.GetChild(1).GetComponent<TMP_Text>().text = initialFillNumber.ToString();
        scoreText = this.gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        goalText = this.gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        scoreText.text = "";
        earnedText = new List<string>();
        goals = new List<string>();
        completeCounter = new List<int>(); completeCounter.Add(0); completeCounter.Add(0); completeCounter.Add(0); completeCounter.Add(0);
        goalInitializing();
    }

    public void useReset()
    {
        if(initialResetNumber > 0)
        {
            initialResetNumber -= 1;
            resetButton.transform.GetChild(1).GetComponent<TMP_Text>().text = initialResetNumber.ToString();
        }
    }

    public void useFill()
    {
        if (initialFillNumber > 0)
        {
            initialFillNumber -= 1;
            fillButton.transform.GetChild(1).GetComponent<TMP_Text>().text = initialFillNumber.ToString();
        }
    }

    public void addBlock(List<string> combo)
    {
        if(combo.Count > 0)
        {
            for(int i = 0; i < combo.Count; i++)
            {
                earnedText.Add(combo[i]);
            }
        }
        recordBlock();
    }

    void recordBlock()
    {
        scoreText.text = "";
        if(earnedText.Count > 0)
        {
            for(int i = 0; i < earnedText.Count; i++)
            {
                //scoreText.text += earnedText[i] + " ";
            }
        }
        refreshScore();
    }

    public void goalInitializing()
    {
        int dice = 0;

        for(int i = 2; i < 6; i++)
        {
            dice = Random.Range(1, 5);
            if (dice == 1)
            {
                if (i == 2)
                    goals.Add("onepair");
                else if (i == 3)
                    goals.Add("triple");
                else if (i == 4)
                    goals.Add("fourcard");
                else if (i == 5)
                    goals.Add("fivecard");
            }
            else if (dice == 2)
            {
                if (i == 2)
                    goals.Add("flush2");
                else if (i == 3)
                    goals.Add("flush3");
                else if (i == 4)
                    goals.Add("flush4");
                else if (i == 5)
                    goals.Add("flush5");
            }
            else if (dice == 3)
            {
                if (i == 2)
                    goals.Add("straight2");
                else if (i == 3)
                    goals.Add("straight3");
                else if (i == 4)
                    goals.Add("straight4");
                else if (i == 5)
                    goals.Add("straight5");
            }
            else if (dice == 4)
            {
                if (i == 2)
                    goals.Add("straightflush2");
                else if (i == 3)
                    goals.Add("straightflush3");
                else if (i == 4)
                    goals.Add("straightflush4");
                else if (i == 5)
                    goals.Add("straightflush5");
            }
        }

        refreshScore();
    }

    public void refreshScore()
    {
        goalText.text = "";
        for (int i = 0; i < goals.Count; i++)
        {
            goalText.text += goals[i] + "(" + howmanyContain(earnedText, goals[i], i) + " / " + howmuchFind(i) + ")" + "\n";
        }
        if (completeCounter[0] * completeCounter[1] * completeCounter[2] * completeCounter[3] == 1)
        {
            loadClearScene.loadClearScene();
        }
    }

    int howmanyContain(List<string> list, string element, int index)
    {
        int sum = 0;

        for(int i = 0; i<list.Count; i++)
        {
            if (list[i] == element)
                sum += 1;
        }

        if(sum >= howmuchFind(index))
        {
            sum = howmuchFind(index);
            completeCounter[index] = 1;
        }

        return sum;
    }

    int howmuchFind(int goal)
    {
        int result = 0;

        result = goalCounts[goal];

        /*
        switch (goal)
        {
            case 0: result = 10; break;
            case 1: result = 5; break;
            case 2: result = 5; break;
            case 3: result = 3; break;
        }
        */

        return result;
    }

}

