using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Nicholaos And Eduardo Worked on this Script 

public class FighterStatus : MonoBehaviour
{
    [Header("Player Manager")]
    public bool dead = false;
    public bool hasSpecial = false;
    public int specialWeight = 1;
    public float specialCounter = 0f;
    public float specialCounterRate = 15f;
    private MyCharacterController controller;
    private FighterAnimation fighterAnimation;

    [Header("Stats")]
    [SerializeField] private SettingsSelection settingsSelection;
    public int playerID;
    public string playerName;
    public float health;
    public float specialPoints;
    public float punchDamage;
    public float kickDamage;
    public float specialDamage;
    public float specialGainWhenHit = 5f;

    [Header("Move LockOut")]
    public float punchLockOut;
    public float kickLockOut;
    public float specialLockOut;
    public float hitStun;
    public float blockStun;
    public float playerPushBack = 10f;
    public float playerPushbackOnBlock = 20f;

    void Start()
    {
        settingsSelection = SettingsSelection.instance;
        CastComponents();
        GetStats();
    }

    private void FixedUpdate()
    {
        SpecialUpdate();
    }

    private void CastComponents()
    {
        controller = gameObject.GetComponent<MyCharacterController>();
        fighterAnimation = gameObject.GetComponent<FighterAnimation>();
    }

    public void GetStats()
    {
        specialPoints = 0;
        health = settingsSelection.health;
        punchDamage = settingsSelection.punchDamage;
        kickDamage = settingsSelection.kickDamage;
    }

    public void ReceiveDamage(float damage)
    {
        if (controller.isBlocking)
        {
            StartCoroutine(HitStunBlockStunLockOut(blockStun));
            controller.PlaySFX("Sharp Punch");
            PushBackOnBlock();
            fighterAnimation.BlockedHitAnimation();
            return;
        }
        else
        {
            health -= damage;
            SpecialOnHit(specialGainWhenHit);
            PushBack();
            controller.PlaySFX("Sharp Punch");
            StartCoroutine(HitStunBlockStunLockOut(hitStun));
            fighterAnimation.TorsoHitAnimation();

            if (health <= 0)
            {
                dead = true;
                controller.DeadWithNoControl();
                fighterAnimation.DeadAnimation();
            }
        }
    }

    public void VictoryDance()
    {
        fighterAnimation.VictoryAnimation();
    }

    public void SpecialUpdate()
    {
        SpecialCounter();

        if(specialPoints < settingsSelection.specialPoints)
        {
            specialPoints += specialCounter / specialWeight / 120;
        }
        else
        {
            hasSpecial = true;
            specialPoints = settingsSelection.specialPoints;
        }
    }

    public void ResetSpecialAttack()
    {
        specialPoints = 0;
        hasSpecial = false;
        specialWeight++;
        specialCounter = 0;
    }

    public void SpecialCounter()
    {
        specialCounter += Time.deltaTime;
    }

    public void SpecialOnHit(float specialGained)
    {
        specialPoints += specialGained;
    }

    public bool HasSpecial()
    {
        return hasSpecial;
    }

    public bool GetDead()
    {
        return dead;
    }

    public void PushBack()
    {
        if (gameObject.transform.root.CompareTag("Player1"))    
        {
            controller.PushedBack(playerPushBack);
        }

        if (gameObject.transform.root.CompareTag("Player2"))
        {
            controller.PushedBack(-playerPushBack);
        }
    }

    public void PushBackOnBlock()
    {
        if (gameObject.transform.root.CompareTag("Player1"))
        {
            controller.PushedBack(playerPushbackOnBlock);
        }

        if (gameObject.transform.root.CompareTag("Player2"))
        {
            controller.PushedBack(-playerPushbackOnBlock);
        }
    }

    private IEnumerator HitStunBlockStunLockOut(float freezeTime)   
    {
        float time = freezeTime;
        controller.isFrozen = true;
        controller.inputHor = 0;
        controller.inputVer = 0;

        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        controller.isFrozen = false;
        yield break;
    }
}
