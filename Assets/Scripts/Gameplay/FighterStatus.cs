using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterStatus : MonoBehaviour
{
    private GameObject GameManager;

    private bool dead = false;

    private bool inBlock = false;

    [Header("UI")]
    public Image healthBar;
    public Text ratioText;

    [Header("Stats")]
    public float health;
    public float punchDamage;
    public float kickDamage;

    private void Awake()
    {
        GameManager = GameObject.Find("GameManager");
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
            HealthBarUpdate();
            gameObject.GetComponent<FighterAnimation>().TorsoHitAnimation();

            HealthBarUpdate();

            if (health <= 0)
            {
                dead = true;
                gameObject.GetComponent<FighterAnimation>().DeadAnimation();
            }
        }
    }

    public void HealthBarUpdate()
    {
        healthBar.fillAmount = health / 100;
        ratioText.text = (health).ToString("0");
    }

    public bool GetDead()
    {
        return dead;
    }

}
