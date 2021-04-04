using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterStatus : MonoBehaviour
{
    GameManager manager;

    [Header("Player Manager")]
    public bool dead = false;
    public bool hasSpecial = false;
    public int specialWeight = 1;
    public float specialCounter = 0f;

    [Header("Stats")]
    public int playerID;
    public float health;
    public float specialPoints = 0;
    public float punchDamage;
    public float kickDamage;
    public float specialDamage;

    private void Awake()
    {
        manager = GameManager.instance;
        health = 100.0f;
    }

    private void FixedUpdate()
    {
        SpecialUpdate();
    }

    public void ReceiveDamage(float damage)
    {
        if (gameObject.GetComponent<MyCharacterController>().isBlocking)
        {
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

        if(specialPoints < 100)
        {
            specialPoints += specialCounter / specialWeight / 120;
        }
        else
        {
            hasSpecial = true;
            specialPoints = 100;
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
