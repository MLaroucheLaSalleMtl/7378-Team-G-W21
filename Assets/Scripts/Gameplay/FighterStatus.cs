using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterStatus : MonoBehaviour
{
    [Header("Player Manager")]
    public bool dead = false;
    public bool hasSpecial = false;
    public int specialWeight = 1;
    public float specialCounter = 0f;
    public float specialCounterRate = 15f;

    [Header("Stats")]
    [SerializeField] private SettingsSelection settingsSelection;
    public int playerID;
    public float health;
    public float specialPoints;
    public float punchDamage;
    public float kickDamage;
    public float specialDamage;

    [Header("Move LockOut")]
    public float punchLockOut;
    public float kickLockOut;
    public float specialLockOut;

    void Start()
    {
        settingsSelection = SettingsSelection.instance;
        GetStats();
    }

    private void FixedUpdate()
    {
        SpecialUpdate();
    }

    public void GetStats()
    {
        specialPoints = 0;
        health = settingsSelection.health;
        punchDamage = settingsSelection.punchDamage;
        kickDamage = settingsSelection.kickDamage;
        specialDamage = settingsSelection.specialDamage;
    }

    public void ReceiveDamage(float damage)
    {
        if (gameObject.GetComponent<MyCharacterController>().isBlocking)
        {
            gameObject.GetComponent<FighterAnimation>().BlockedHitAnimation();
            return;
        }
        else
        {
            health -= damage;
            gameObject.GetComponent<MyCharacterController>().PushedBack();
            gameObject.GetComponent<FighterAnimation>().TorsoHitAnimation();

            if (health <= 0)
            {
                dead = true;
                gameObject.GetComponent<FighterAnimation>().DeadAnimation();
            }
        }
    }

    public void SpecialUpdate()
    {
        SpecialCounter();

        if(specialPoints < settingsSelection.specialPoints)
        {
            specialPoints += specialCounter / specialWeight / 120;
        }
        else
        {
            hasSpecial = true;
            specialPoints = settingsSelection.specialPoints;
        }
    }

    public void ResetSpecialAttack()
    {
        specialPoints = 0;
        hasSpecial = false;
        specialWeight++;
        specialCounter = 0;
    }

    public void SpecialCounter()
    {
        specialCounter += Time.deltaTime;
    }

    public bool HasSpecial()
    {
        return hasSpecial;
    }

    public bool GetDead()
    {
        return dead;
    }
}
