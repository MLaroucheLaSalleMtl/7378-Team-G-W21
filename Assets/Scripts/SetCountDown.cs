using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCountDown : MonoBehaviour
{
    private GameManager2 GM2;

    public void SetCountDownNow()
    {
        GM2 = GameObject.Find("GameManager").GetComponent<GameManager2>();
        GM2.countDown = true;
    }


}
