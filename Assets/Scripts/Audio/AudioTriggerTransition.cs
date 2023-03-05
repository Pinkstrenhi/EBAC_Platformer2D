using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTriggerTransition : MonoBehaviour
{
    public AudioMixerSnapshot snapshot01;
    public AudioMixerSnapshot snapshot02;
    public string stringToCompare = "Player";
    private float timeToTransition = 0.1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag(stringToCompare))
        {
            snapshot02.TransitionTo(timeToTransition);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag(stringToCompare))
        {
            snapshot01.TransitionTo(timeToTransition);
        } 
    }
}
