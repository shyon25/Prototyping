using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayButton : MonoBehaviour
{
    public GameObject Image;
    public void turnOnImage()
    {
        Image.SetActive(true);
    }

    public void turnOffImage()
    {
        Image.SetActive(false);
    }
}
