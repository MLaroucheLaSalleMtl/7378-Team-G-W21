using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Nicholaos Sifakis worked on this script

public class AttackSystem : MonoBehaviour
{
    [Header("Player Manager")]
    public Collider[] colliders;
    private GameObject otherPlayer;
    private float damage = 0;
    [SerializeField] private float specialGain = 15f;
    private FighterStatus fighterStatus;
    private MyCharacterController controller;

    [Space]
    [Header ("Projectiles")]
    [SerializeField] private GameObject fireball;
    [SerializeField] private GameObject fireballSpawn;

    private void Awake()
    {
        CastComponents();
    }

    private void CastComponents()
    {
        fighterStatus = gameObject.GetComponent<FighterStatus>();
        controller = gameObject.GetComponent<MyCharacterController>();
    }

    private void LaunchAttackOne() 
    {   
        var cols = Physics.OverlapBox(colliders[0].bounds.center, colliders[0].bounds.extents, colliders[0].transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform) //used to check if i'm hitting myself 
            {
                continue;
            }

            if (c.transform.root.CompareTag("Player1") || c.transform.root.CompareTag("Player2"))  //it works when the scrip is attached to player1, so otherPlayer is Player2
            {
                otherPlayer = c.transform.root.gameObject;
            }

            //gameObject -> attacking //otherPlayer -> being attacked
            otherPlayer.GetComponent<FighterStatus>().ReceiveDamage(fighterStatus.punchDamage);
            fighterStatus.SpecialOnHit(specialGain);
        }
    }

    private void LaunchAttackTwo()
    {
        var cols = Physics.OverlapBox(colliders[1].bounds.center, colliders[1].bounds.extents, colliders[1].transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform) //used to check if i'm hitting myself 
            {
                continue;
            }

            if (c.transform.root.CompareTag("Player1") || c.transform.root.CompareTag("Player2"))  //it works when the scrip is attached to player1, so otherPlayer is Player2
            {
                otherPlayer = c.transform.root.gameObject;
            }

            //gameObject -> attacking //otherPlayer -> being attacked

            otherPlayer.GetComponent<FighterStatus>().ReceiveDamage(fighterStatus.kickDamage);
            fighterStatus.SpecialOnHit(specialGain);
        }
    }
    
    private void LaunchSpecialAttack()
    {
        var cols = Physics.OverlapBox(colliders[2].bounds.center, colliders[2].bounds.extents, colliders[2].transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform) //used to check if i'm hitting myself 
            {
                continue;
            }

            if (c.transform.root.CompareTag("Player1") || c.transform.root.CompareTag("Player2"))  //it works when the scrip is attached to player1, so otherPlayer is Player2
            {
                otherPlayer = c.transform.root.gameObject;
            }

            //gameObject -> attacking //otherPlayer -> being attacked

            otherPlayer.GetComponent<FighterStatus>().ReceiveDamage(fighterStatus.specialDamage);
        }
    }

    private void LaunchFireball()
    {
        controller.PlaySFX("Small Fireball");
        Instantiate(fireball, fireballSpawn.transform.position, Quaternion.identity);
    }
}