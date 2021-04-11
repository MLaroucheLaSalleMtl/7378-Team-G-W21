using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTracker : MonoBehaviour
{
    public GameObject[] roundCounterP1Image;
    public GameObject[] roundCounterP2Image;

    void Start()
    {
        for (int i = 0; i < GameManager.instance.roundCounterP1; i++)    
        {
            roundCounterP1Image[i].SetActive(true);
        }

        for (int i = 0; i < GameManager.instance.roundCounterP2; i++)
        {
            roundCounterP2Image[i].SetActive(true);
        }
    }
}
