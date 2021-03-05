using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public Collider[] colliders;

    private GameObject otherPlayer;

    private float damage = 0;

    private void LaunchAttackOne() 
    {
        damage = 20f;

        var cols = Physics.OverlapBox(colliders[0].bounds.center, colliders[0].bounds.extents, colliders[0].transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {

            if (c.transform.root == transform) //used to check if i'm hitting myself 
            {
                continue;
            }

            if (c.transform.root.CompareTag("Player2"))  //it works when the scrip is attached to player1, so otherPlayer is Player2
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
        damage = 25f;

        var cols = Physics.OverlapBox(colliders[1].bounds.center, colliders[1].bounds.extents, colliders[1].transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform) //used to check if i'm hitting myself 
            {
                continue;
            }

            if (c.transform.root.CompareTag("Player2"))  //it works when the scrip is attached to player1, so otherPlayer is Player2
            {
                Debug.Log(c.transform.root.name);
                otherPlayer = c.transform.root.gameObject;
            }

            Debug.Log(c.name);

            //gameObject -> attacking //otherPlayer -> being attacked

            otherPlayer.GetComponent<FighterStatus>().ReceiveDamage(damage);

        }
    }

    public Collider HitLocation(Collider hurtBox)
    {
        /*
        switch (hurtBox.gameObject.name)
        {
            case "Head_HurtBox":
                Debug.Log(hurtBox.name);
                return hurtBox;
            case "Torso_HurtBox":
                Debug.Log(hurtBox.name);
                return hurtBox;
            case "Leg_HurtBox":
                Debug.Log(hurtBox.name);
                return hurtBox;
            default:
                Debug.Log(hurtBox.name);
                return hurtBox;
        }
        */

        if(hurtBox.gameObject.name == "Head_HurtBox")
        {
            Debug.Log(hurtBox.name);
            return hurtBox;
        }
        else if (hurtBox.gameObject.name == "Torso_HurtBox")
        {
            Debug.Log(hurtBox.name);
            return hurtBox;
        }
        else
        {
            Debug.Log(hurtBox.name);
            return hurtBox;
        }
    }
}

// how to do damage
//float damage = 0; 
//switch (c.name)
//{
//    case "Head":
//        damage = 30;
//        break;
//    case "torso":
//        damage = 10;
//        break;
//    default:
//        Debug.Log("Unable to identify body part, name must match case");
//        break;
//}
