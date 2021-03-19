using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterStatus : MonoBehaviour
{
    GameManager manager;
    private bool dead = false;

    [Header("Stats")]
    public float health;
    public float punchDamage;
    public float kickDamage;
    public float specialDamage;

    private void Awake()
    {
        manager = GameManager.instance;
        health = 100.0f;
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
            gameObject.GetComponent<FighterAnimation>().TorsoHitAnimation();

            if (health <= 0)
            {
                dead = true;
                gameObject.GetComponent<FighterAnimation>().DeadAnimation();
            }
        }
    }

    public bool GetDead()
    {
        return dead;
    }
}
