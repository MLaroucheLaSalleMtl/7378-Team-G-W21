using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    private float verticalVelocity;

    public Collider[] attackHitBoxes;
    public Collider[] attackHurtBoxes;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))    
        {
            LaunchAttack(attackHitBoxes[0]);
            LaunchAttack(attackHurtBoxes[0]);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            LaunchAttack(attackHitBoxes[1]);
            LaunchAttack(attackHurtBoxes[1]);
        }

        if (controller.isGrounded)
        {
            verticalVelocity = -1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = 10;
            }
        }
        else
        {
            verticalVelocity -= 14 * Time.deltaTime;
        }

        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal") * 5;
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
        
    }

    private void LaunchAttack(Collider col)
    {
        var cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach(Collider c in cols)
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
