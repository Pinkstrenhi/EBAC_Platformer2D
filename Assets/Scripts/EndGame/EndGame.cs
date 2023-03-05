using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public string stringToCompare = "Player";
    public GameObject uiEndGame;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag(stringToCompare))
        {
            CallEndGame();
        }
    }

    public void CallEndGame()
    {
        uiEndGame.SetActive(true);
    }
}
