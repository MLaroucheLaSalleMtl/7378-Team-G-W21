using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Eduardo Worked on this Script

public class SettingsSelection : MonoBehaviour
{
    public static SettingsSelection instance = null;
    [SerializeField] private SelectMenu selectMenu;

    [Header("Settings Manager")]
    public float timer;
    public float health;
    public float specialPoints;
    public float punchDamage;
    public float kickDamage;
    public float specialDamage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        selectMenu = SelectMenu.instance;
        StandardStats();
    }

    public void StandardStats()
    {
        timer = 99;
        health = 100;
        specialPoints = 100;
        punchDamage = 10;
        kickDamage = 15;
        specialDamage = 25;
}

    public void OnTimerSelection(int val)
    {
        if(val == 0)
        {
            timer = 99;
        }
        else if(val == 1)
        {
            timer = 60;
        }
        else if(val == 2)
        {
            timer = 90;
        }
        else if (val == 3)
        {
            timer = 120;
        }
    }

    public void OnSpecialPointsSelection(int val)
    {
        if (val == 0)
        {
            specialPoints = 100;
        }
        else if (val == 1)
        {
            specialPoints = 80;
        }
        else if (val == 2)
        {
            specialPoints = 120;
        }
    }

    public void OnPlayersHealthSelection(int val)
    {
        if (val == 0)
        {
            health = 100;
        }
        else if (val == 1)
        {
            health = 80;
        }
        else if (val == 2)
        {
            health = 120;
        }
    }

    public void OnPunchSelection(int val)
    {
        if (val == 0)
        {
            punchDamage = 5;
        }
        else if (val == 1)
        {
            punchDamage = 10;
        }
        else if (val == 2)
        {
            punchDamage = 15;
        }
        else if (val == 3)
        {
            punchDamage = 100;
        }
    }

    public void OnKickSelection(int val)
    {
        if (val == 0)
        {
            kickDamage = 10;
        }
        else if (val == 1)
        {
            kickDamage = 15;
        }
        else if (val == 2)
        {
            kickDamage = 20;
        }
        else if (val == 3)
        {
            punchDamage = 100;
        }
    }

    public void OnConfirmation()
    {
        selectMenu.PanelToggle(4);
    }
}
