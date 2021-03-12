using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityBox : MonoBehaviour
{
    private bool isBlocking = false;

    private Collider collider;

    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider>();
    }


    // Update is called once per frame
    void Update()
    {
        var cols = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents, collider.transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {

            if (c.transform.root == transform) //used to check if i'm hitting myself 
            {
                continue;
            }

            Debug.Log("working");

            isBlocking = true;

        }


    }

}
