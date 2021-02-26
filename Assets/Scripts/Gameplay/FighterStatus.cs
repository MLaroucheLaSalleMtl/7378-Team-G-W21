using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterStatus : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Stats")]
    public float health;
    public float punch;
    public float kick;

    private bool dead = false;

    private GameObject GameManager;

    private void Awake()
    {
        GameManager = GameObject.Find("GameManager");
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        //plays ReceivingDamage animation

        if (health <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";
            //plays Death animation
        }
    }

}
