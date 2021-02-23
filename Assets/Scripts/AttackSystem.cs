using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public Collider[] colliders;

    private float damage = 0;

    private void LaunchAttackOne() 
    {

        damage = 20f;

        var cols = Physics.OverlapBox(colliders[0].bounds.center, colliders[0].bounds.extents, colliders[0].transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {

            if (c.transform.root == transform) //used to check if im hitting my self 
            {
                continue;
            }

            Debug.Log(c.name);

            GameManager.instance.ReceivingDamage(damage, gameObject);
            
        }

    }

    private void LaunchAttackTwo()
    {
        var cols = Physics.OverlapBox(colliders[1].bounds.center, colliders[1].bounds.extents, colliders[1].transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform) //used to check if im hitting my self 
            {
                continue;
            }

            Debug.Log(c.name);

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
