using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private List<FighterStatus> fighterStatus;

    //Properties
    public float timeCounter = 0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy (gameObject);
        }
    }

    void Start()
    {
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        FighterStatus currentP1FighterStatus = player1.GetComponent<FighterStatus>();
        fighterStatus.Add(currentP1FighterStatus);

        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        FighterStatus currentP2FighterStatus = player2.GetComponent<FighterStatus>();
        fighterStatus.Add(currentP2FighterStatus);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            ExitGame();
        }
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
