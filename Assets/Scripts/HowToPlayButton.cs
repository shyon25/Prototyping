using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayButton : MonoBehaviour
{
    public int pageIndex;
    public GameObject Image;
    public List<Sprite> pages;

    private void Start()
    {
        pageIndex = -1;
    }

    public void turnOnImage()
    {
        Image.SetActive(true);
        if(pageIndex < pages.Count - 1)
        {
            pageIndex++;
            Image.GetComponent<Image>().sprite = pages[pageIndex];
        }
    }

    public void turnOffImage()
    {
        Image.SetActive(false);
        pageIndex = -1;
    }

    public void previousImage()
    {
        if(pageIndex > 0)
        {
            pageIndex--;
            Image.GetComponent<Image>().sprite = pages[pageIndex];
        }
    }
}
