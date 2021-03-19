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
    public float inputHor, inputVer = 0f;
    private bool inputJump = false;
    private bool inputPunch = false;
    private bool inputKick = false;
    private bool inputSpecialAttack = false;
    [SerializeField] private bool isFrozen = false;
    [SerializeField] private float inputTimer = 0.5f;
    public bool isBlocking = false;

    // Input methods
    public void SetMove(Vector2 move)
    {
        if (isFrozen)
        {
            return;
        }
        inputHor = move.x;
        inputVer = move.y;
    }

    public void SetJump(bool jump)
    {
        inputJump = jump;
    }

    public void SetPunch(bool punch)
    {
        if (isFrozen)
        {
            return;
        }
        inputPunch = punch;
    }

    public void SetKick(bool kick)
    {
        if (isFrozen)
        {
            return;
        }

        inputKick = kick;
    }

    public void SetSpecialAttack(bool specialAttack)
    {
        inputSpecialAttack = specialAttack;
    }

    public void SetBlocking(bool blocking)
    {
        if (isFrozen)
        {
            return;
        }
        isBlocking = blocking;
        gameObject.GetComponent<FighterAnimation>().BlockAnimation(isBlocking);
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

    void FixedUpdate()
    {
        if (inputPunch)
        {
            inputTimer = 0.8f;
            StartCoroutine(AnimationRoutine(inputTimer));
            gameObject.GetComponent<FighterAnimation>().PunchAnimation();
            inputPunch = false;
        }

        if (inputKick)
        {
            inputTimer = 1.5f;
            StartCoroutine(AnimationRoutine(inputTimer));
            gameObject.GetComponent<FighterAnimation>().KickAnimation();
            inputKick = false;
        }

        if (inputSpecialAttack)
        {
            gameObject.GetComponent<FighterAnimation>().SpecialAnimation();
            inputSpecialAttack = false;
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

    //public void FreezePlayerControl()
    //{
    //    isFrozen = true;
    ////inputHor = 0;
    ////    inputVer = 0;
    //}

    //public void UnFreezePlayerControl()
    //{
    //    isFrozen = false;
    //}

    private IEnumerator AnimationRoutine(float freezeTime)
    {
        float time = freezeTime;
        isFrozen = true;
        inputHor = 0;
        inputVer = 0;

        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        isFrozen = false;
        yield break;
    }

}


// use lerp to smooth animations 
//[SerializeField] private float lerpTime = 0.05f;

//Vector3 targetVelocity = moveVector * speed;

//targetVelocity.y = verticalVelocity;
//Vector3 nextVelocity = Vector3.Lerp(controller.velocity, targetVelocity, lerpTime * Time.deltaTime);

//controller.Move(nextVelocity * Time.deltaTime);




