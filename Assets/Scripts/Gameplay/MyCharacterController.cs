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

    //Input system 
    public float inputHor, inputVer = 0f;
    public bool isBlocking = false;
    public bool inputPunch = false;
    public bool inputKick = false;
    public bool inputSpecialAttack = false;
    public bool isFrozen = false;
    [SerializeField] private float inputTimer = 0.5f;

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

    public void SetPunch(bool punch)
    {
        if (isFrozen || isBlocking)
        {
            return;
        }

        inputPunch = punch;
    }

    public void SetKick(bool kick)
    {
        if (isFrozen || isBlocking)
        {
            return;
        }

        inputKick = kick;
    }

    public void SetSpecialAttack(bool specialAttack)
    {
        if (isFrozen || isBlocking)
        {
            return;
        }

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
        if (isBlocking == true) 
        {
            inputHor = 0;
            inputVer = 0;
        }

        if (inputPunch)
        {
            inputTimer = gameObject.GetComponent<FighterStatus>().punchLockOut;
            StartCoroutine(AnimationRoutine(inputTimer));
            gameObject.GetComponent<FighterAnimation>().PunchAnimation();
            inputPunch = false;
        }

        if (inputKick)
        {
            inputTimer = gameObject.GetComponent<FighterStatus>().kickLockOut;
            StartCoroutine(AnimationRoutine(inputTimer));
            gameObject.GetComponent<FighterAnimation>().KickAnimation();
            inputKick = false;
        }

        if (inputSpecialAttack)
        {
            inputTimer = gameObject.GetComponent<FighterStatus>().specialLockOut;
            StartCoroutine(AnimationRoutine(inputTimer));
            gameObject.GetComponent<FighterAnimation>().SpecialAnimation();
            gameObject.GetComponent<FighterStatus>().ResetSpecialAttack();
            inputSpecialAttack = false;
        }


        verticalVelocity = -1;
        moveVector = Vector3.zero;
        moveVector = new Vector3(-inputHor, inputVer, 0);
        moveVector *= speed;
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
        Animate();
    }


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

    public void PushedBack(float pushBack)
    {
        Vector3 pushedBack = new Vector3(0 + pushBack, 0, 0);
        controller.SimpleMove(pushedBack);
    }

    public void DeadWithNoControl()
    {
        isFrozen = true;
    }
}






