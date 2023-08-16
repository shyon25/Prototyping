using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStartScene : MonoBehaviour
{
    public void loadStartScene()
    {
        SceneManager.LoadScene("Start");
    }
}
