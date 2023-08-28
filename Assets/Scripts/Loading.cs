using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Loading : MonoBehaviour
{
    public int loading;

    private void Start()
    {
        loading = 0;
    }

   

    public void loadOne(int loadingNumber)
    {
        loading = loadingNumber;
        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "Now Loading... " + loading + "%";
    }
}
