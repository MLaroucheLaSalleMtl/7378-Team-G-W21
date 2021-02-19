using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    //public Collider[] colliders;


    //void Update()
    //{
    //    foreach (Collider collider in colliders)    
    //    {
    //        LaunchAttack(collider);
    //    }
    //}

    private void LaunchAttack(Collider col)
    {
        var cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hurtbox"));
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
