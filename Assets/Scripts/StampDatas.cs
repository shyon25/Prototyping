using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StampDatas : MonoBehaviour
{
    public int number;
    public List<Vector2> coloredPoint;
    public GameObject frame;
    public Color color;
    public int myStampNumber;

    int randomPointx, randomPointy;

    private void Start()
    {
        Vector2 tempPoint;
        coloredPoint = new List<Vector2>();
        for(int i = 0; i<number; i++)
        {
            do
            {
                randomPointx = Random.Range(0, 3);
                randomPointy = Random.Range(0, 3);
                tempPoint = new Vector2(randomPointx, randomPointy);
            } while (coloredPoint.Contains(tempPoint));
            coloredPoint.Add(tempPoint);
        }
        color = randomColor();
    }

    public void changeFrame()
    {
        if(myStampNumber == frame.GetComponent<FrameComponents>().currentStampNumber)
        {
            bool currentActivateState = frame.GetComponent<FrameComponents>().isActivate;
            frame.GetComponent<FrameComponents>().isActivate = !currentActivateState;
        }
        else
        {
            frame.GetComponent<FrameComponents>().isActivate = true;
            frame.GetComponent<FrameComponents>().currentStampNumber = myStampNumber;
            frame.GetComponent<FrameComponents>().coloredPoint = coloredPoint;
        }

        frame.GetComponent<FrameComponents>().changeColors(coloredPoint, color);
    }
    public Color randomColor()
    {
        Color resultColor = new Color();
        int randInt = Random.Range(1, 6);
        switch (randInt)
        {
            case 1:
                resultColor = Color.red;
                break;
            case 2:
                resultColor = Color.green;
                break;
            case 3:
                resultColor = Color.blue;
                break;
            case 4:
                resultColor = Color.white;
                break;
            case 5:
                resultColor = Color.yellow;
                break;
            default:
                break;
        }
        return resultColor;
    }
}
