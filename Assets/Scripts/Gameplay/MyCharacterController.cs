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
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float fallSpeed = 14f;


    //Input system 
    private float inputHor, inputVer = 0f;
    private bool inputJump = false;
    private bool inputAttackOne = false;
    private bool inputAttackTwo = false;
    private bool isFrozen = false;

    // Input methods
    public void OnMove(InputAction.CallbackContext context) // WSAD or left stick
    {
        if (isFrozen)
        {
            return;
        }
        Vector2 v = context.ReadValue<Vector2>();
        inputHor = v.x;
        inputVer = v.y;
    }
    public void OnJump(InputAction.CallbackContext context) // spacekey or south button
    {
        if (isFrozen)
        {
            return;
        }
        inputJump = context.performed;
    }
    public void OnAttackOne(InputAction.CallbackContext context) // U or west button
    {
        if (isFrozen)
        {
            return;
        }
        inputAttackOne = context.performed;
    }
    public void OnAttackTwo(InputAction.CallbackContext context) // I or north button
    {
        if (isFrozen)
        {
            return;
        }
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
                verticalVelocity = jumpSpeed;
            }
        }
        else
        {
            verticalVelocity -= fallSpeed * Time.deltaTime;
        }

        moveVector = Vector3.zero;
        moveVector = new Vector3(-inputHor, inputVer, 0);
        moveVector *= speed;
        moveVector.y = verticalVelocity;


        controller.Move(moveVector * Time.deltaTime);
        Animate();
    }

    public void FreezePlayerControl()
    {
        isFrozen = true;
        inputHor = 0;
        inputVer = 0;
    }

    public void UnFreezePlayerControl()
    {
        isFrozen = false;
    }

}


// use lerp to smooth animations 
//[SerializeField] private float lerpTime = 0.05f;

//Vector3 targetVelocity = moveVector * speed;

//targetVelocity.y = verticalVelocity;
//Vector3 nextVelocity = Vector3.Lerp(controller.velocity, targetVelocity, lerpTime * Time.deltaTime);

//controller.Move(nextVelocity * Time.deltaTime);




