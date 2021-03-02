using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterStatus : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool dead = false;

    private GameObject GameManager;

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
        health -= damage;
        HealthBarUpdate();
        animator.SetTrigger("isHit");
        Debug.Log("Got hit!");

        HealthBarUpdate();

        if (health <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";
            Debug.Log("Dead");
            animator.SetBool("isDead", true);
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
