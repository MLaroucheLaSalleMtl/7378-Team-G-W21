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

    [Header("Stats")]
    [SerializeField] private SettingsSelection settingsSelection;
    public int playerID;
    public string playerName;
    public float health;
    public float specialPoints;
    public float punchDamage;
    public float kickDamage;
    public float specialDamage;
    public float sepcialGainWhenHit = 5f;

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
        GetStats();
    }

    private void FixedUpdate()
    {
        SpecialUpdate();
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
        if (gameObject.GetComponent<MyCharacterController>().isBlocking)
        {
            StartCoroutine(HitStunBlockStunLockOut(blockStun));
            gameObject.GetComponent<MyCharacterController>().PlaySFX("Sharp Punch");
            PushBackOnBlock();
            gameObject.GetComponent<FighterAnimation>().BlockedHitAnimation();
            return;
        }
        else
        {
            health -= damage;
            SpecialOnHit(sepcialGainWhenHit);
            PushBack();
            gameObject.GetComponent<MyCharacterController>().PlaySFX("Sharp Punch");
            StartCoroutine(HitStunBlockStunLockOut(hitStun));
            gameObject.GetComponent<FighterAnimation>().TorsoHitAnimation();

            if (health <= 0)
            {
                dead = true;
                gameObject.GetComponent<MyCharacterController>().DeadWithNoControl();
                gameObject.GetComponent<FighterAnimation>().DeadAnimation();
            }
        }
    }

    public void VictoryDance()
    {
        gameObject.GetComponent<FighterAnimation>().VictoryAnimation();
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
            gameObject.GetComponent<MyCharacterController>().PushedBack(playerPushBack);
        }

        if (gameObject.transform.root.CompareTag("Player2"))
        {
            gameObject.GetComponent<MyCharacterController>().PushedBack(-playerPushBack);
        }
    }

    public void PushBackOnBlock()
    {
        if (gameObject.transform.root.CompareTag("Player1"))
        {
            gameObject.GetComponent<MyCharacterController>().PushedBack(playerPushbackOnBlock);
        }

        if (gameObject.transform.root.CompareTag("Player2"))
        {
            gameObject.GetComponent<MyCharacterController>().PushedBack(-playerPushbackOnBlock);
        }
    }

    private IEnumerator HitStunBlockStunLockOut(float freezeTime)   
    {
        float time = freezeTime;
        gameObject.GetComponent<MyCharacterController>().isFrozen = true;
        gameObject.GetComponent<MyCharacterController>().inputHor = 0;
        gameObject.GetComponent<MyCharacterController>().inputVer = 0;

        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        gameObject.GetComponent<MyCharacterController>().isFrozen = false;
        yield break;
    }
}
