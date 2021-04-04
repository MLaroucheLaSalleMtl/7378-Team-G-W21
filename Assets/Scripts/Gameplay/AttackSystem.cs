using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public Collider[] colliders;

    private GameObject otherPlayer;
    private float damage = 0;

    [SerializeField] private GameObject fireball;
    [SerializeField] private GameObject fireballSpawn;

    private void LaunchAttackOne() 
    {
        damage = gameObject.GetComponent<FighterStatus>().punchDamage;

        var cols = Physics.OverlapBox(colliders[0].bounds.center, colliders[0].bounds.extents, colliders[0].transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform) //used to check if i'm hitting myself 
            {
                continue;
            }

            if (c.transform.root.CompareTag("Player1") || c.transform.root.CompareTag("Player2"))  //it works when the scrip is attached to player1, so otherPlayer is Player2
            {
                Debug.Log(c.transform.root.name);
                otherPlayer = c.transform.root.gameObject;
            }

            Debug.Log(c.name);

            //gameObject -> attacking //otherPlayer -> being attacked

            otherPlayer.GetComponent<FighterStatus>().ReceiveDamage(damage);
        }
    }

    private void LaunchAttackTwo()
    {
        damage = gameObject.GetComponent<FighterStatus>().kickDamage;

        var cols = Physics.OverlapBox(colliders[1].bounds.center, colliders[1].bounds.extents, colliders[1].transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform) //used to check if i'm hitting myself 
            {
                continue;
            }

            if (c.transform.root.CompareTag("Player1") || c.transform.root.CompareTag("Player2"))  //it works when the scrip is attached to player1, so otherPlayer is Player2
            {
                Debug.Log(c.transform.root.name);
                otherPlayer = c.transform.root.gameObject;
            }

            Debug.Log(c.name);

            //gameObject -> attacking //otherPlayer -> being attacked

            otherPlayer.GetComponent<FighterStatus>().ReceiveDamage(damage);
        }
    }
    
    private void LaunchSpecialAttack()
    {
        damage = gameObject.GetComponent<FighterStatus>().specialDamage;

        var cols = Physics.OverlapBox(colliders[1].bounds.center, colliders[1].bounds.extents, colliders[1].transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform) //used to check if i'm hitting myself 
            {
                continue;
            }

            if (c.transform.root.CompareTag("Player1") || c.transform.root.CompareTag("Player2"))  //it works when the scrip is attached to player1, so otherPlayer is Player2
            {
                Debug.Log(c.transform.root.name);
                otherPlayer = c.transform.root.gameObject;
            }

            Debug.Log(c.name);

            //gameObject -> attacking //otherPlayer -> being attacked

            otherPlayer.GetComponent<FighterStatus>().ReceiveDamage(damage);
        }
    }

    private void LaunchFireball()
    {
        Instantiate(fireball, fireballSpawn.transform.position, Quaternion.identity);
    }
}