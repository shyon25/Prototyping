using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClick : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip click;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = click;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    public void playClick()
    {
        audioSource.Play();
    }
}
