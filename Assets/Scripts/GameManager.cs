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

    //Health Bar Player 1
    public Image currentHealthBarOne;
    public Text ratioTextOne;

    //Health Bar Player 2
    public Image currentHealthBarTwo;
    public Text ratioTextTwo;

    //Properties
    public float DamageTaken { get; set; }
    public float CurrentHealthPlayer1 { get; set; } = 100f;
    public float CurrentHealthPlayer2 { get; set; } = 100f;
    public bool IsDeadBool { get; set; } = false;
    public float timeCounter = 0f;

    //------------------- Definitive GM (in progresss...) ------------------------

    private List<FighterStatus> fighterStatus;

    //----------------------------------------------------------------------------

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
        IsDeadBool = false;
    }


    void Update()
    {
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
        if(CurrentHealthPlayer1 == 0)
        {
            IsDead();
        }

        HealthPTwo();
        if(CurrentHealthPlayer2 == 0)
        {
            IsDead();
        }
    }


    public void ReceivingDamage(float damage, GameObject obj, GameObject obj2)
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
        IsDeadBool = true;
        //plays death animation
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
