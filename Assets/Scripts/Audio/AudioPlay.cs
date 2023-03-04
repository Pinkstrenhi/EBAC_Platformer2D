using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.P;
    public AudioSource audioSource;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            Play();
        }
    }

    private void Play()
    {
        audioSource.Play();
    }
}
