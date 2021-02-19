using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyCharacterController : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;
    private Vector3 moveVector;
    private float verticalVelocity;
    [SerializeField] private float speed = 6f;


    //input system 
    private float inputHor, inputVer = 0f;
    private bool inputJump = false;
    private bool inputAttackOne = false;
    private bool inputAttackTwo = false;

    // Input methods
    public void OnMove(InputAction.CallbackContext context) // WSAD or left stick
    {
        Vector2 v = context.ReadValue<Vector2>();
        inputHor = v.x;
        inputVer = v.y;
    }
    public void OnJump(InputAction.CallbackContext context) // spacekey or south button
    {
        inputJump = context.performed;
    }
    public void OnAttackOne(InputAction.CallbackContext context) // U or west button
    {
        inputAttackOne = context.performed;
    }
    public void OnAttackTwo(InputAction.CallbackContext context) // I or north button
    {
        inputAttackTwo = context.performed;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Animate()
    {
        anim.SetFloat("Magnitude", controller.velocity.magnitude);
        anim.SetFloat("Hor", moveVector.x);
        anim.SetFloat("Ver", moveVector.y);
    }

    void Update()
    {

        if (inputAttackOne)
        {
            anim.SetTrigger("AttackOne");
            inputAttackOne = false;
        }
        if (inputAttackTwo)
        {
            anim.SetTrigger("AttackTwo");
            inputAttackTwo = false;
        }

        if (controller.isGrounded)
        {
            verticalVelocity = -1;
            if (inputJump)
            {
                verticalVelocity = 10;
            }
        }
        else
        {
            verticalVelocity -= 14 * Time.deltaTime;
        }

        moveVector = Vector3.zero;
        moveVector = new Vector3(-inputHor, inputVer, 0);
        moveVector *= speed;
        moveVector.y = verticalVelocity;


        controller.Move(moveVector * Time.deltaTime);
        Animate();

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



