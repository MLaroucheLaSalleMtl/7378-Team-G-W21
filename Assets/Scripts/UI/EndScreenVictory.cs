using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenVictory : MonoBehaviour
{
    public GameObject endScreen;

    void Update()
    {
        if (GameManager.instance.matchFullyEnded)
        {
            endScreen.SetActive(true);
        }
        else
        {
            endScreen.SetActive(false);
        }
    }

}
