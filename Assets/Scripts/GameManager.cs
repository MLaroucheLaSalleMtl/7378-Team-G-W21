using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    //Players
    public GameObject player1;
    public GameObject player2;


    //Health Bars
    public Image currentHealthBarOne;
    public Text ratioTextOne;

    public Image currentHealthBarTwo;
    public Text ratioTextTwo;



    //Properties
    public float DamageTaken { get; set; }
    public float CurrentHealthPlayer1 { get; set; } = 100f;
    public float CurrentHealthPlayer2 { get; set; } = 100f;
    public bool isDead { get; set; } = false;


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
        isDead = false;
    }


    void Update()
    {
        IsDead();
        UpdateHealth();

        if (Input.GetKey("escape"))
        {
            ExitGame();
        }
    }


    public void HealthPOne()
    {
        currentHealthBarTwo.fillAmount = CurrentHealthPlayer2 / 100;
        ratioTextTwo.text = (CurrentHealthPlayer2).ToString("0");
    }
    public void HealthPTwo()
    {
        currentHealthBarOne.fillAmount = CurrentHealthPlayer1 / 100;
        ratioTextOne.text = (CurrentHealthPlayer1).ToString("0");
    }

    public void UpdateHealth()
    {
        HealthPOne();
        HealthPTwo();
    }


    public void ReceivingDamage(float damage, GameObject obj)
    {
        DamageTaken = damage;

        if (obj == player1)
        {
            CurrentHealthPlayer2 -= DamageTaken;
        }
        else
        {
            CurrentHealthPlayer1 -= DamageTaken;
        }
    }

    public void IsDead()
    {
        if (CurrentHealthPlayer1 == 0)
        {
            isDead = true;
        }
        if (CurrentHealthPlayer2 == 0)
        {
            isDead = true;
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
