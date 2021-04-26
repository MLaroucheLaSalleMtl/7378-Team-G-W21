using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Nicholaos And Eduardo Worked on this Script 

public class MyCharacterController : MonoBehaviour
{
    [Header("Player Manager")]
    private CharacterController controller;
    private Animator anim;
    private FighterAnimation fighterAnimation;
    private FighterStatus fighterStatus;
    private Vector3 moveVector;
    private float verticalVelocity;
    private AudioSource playerSFXSource;
    [SerializeField] private float speed = 6f;

    [Header("Input System")]
    public float inputHor, inputVer = 0f;
    public bool isBlocking = false;
    public bool inputPunch = false;
    public bool inputKick = false;
    public bool inputSpecialAttack = false;
    public bool isFrozen = false;
    [SerializeField] private float inputTimer = 0.5f;

    void Start()
    {
        CastComponents();
    }

    private void CastComponents()
    {
        fighterStatus = gameObject.GetComponent<FighterStatus>();
        fighterAnimation = gameObject.GetComponent<FighterAnimation>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerSFXSource = GetComponent<AudioSource>();
    }

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
        fighterAnimation.BlockAnimation(isBlocking);
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
            PlaySFX("Woosh");
            StartCoroutine(AnimationRoutine(fighterStatus.punchLockOut));
            fighterAnimation.PunchAnimation();
            inputPunch = false;
        }

        if (inputKick)
        {
            PlaySFX("Woosh");
            StartCoroutine(AnimationRoutine(fighterStatus.kickLockOut));
            fighterAnimation.KickAnimation();
            inputKick = false;
        }

        if (inputSpecialAttack)
        {
            StartCoroutine(AnimationRoutine(fighterStatus.specialLockOut));
            fighterAnimation.SpecialAnimation();
            fighterStatus.ResetSpecialAttack();
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

    public void VictoryWithNoControl()
    {
        isFrozen = true;
    }

    public void IsBlockingFemale()
    {
        isBlocking = true;
    }
  
    public void PlaySFX(string clipName)
    {
        AudioClip clipToPlay = FindObjectOfType<SoundManager>().GetSFX(clipName);
        if (clipToPlay != null)
        {
            playerSFXSource.PlayOneShot(clipToPlay);
        }
    }
}