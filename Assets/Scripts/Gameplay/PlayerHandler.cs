using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    GameManager manager;
    private MyCharacterController player = null;

    private bool inputJump = false;
    private bool inputPunch = false;
    private bool inputKick = false;
    private bool inputSpecialAttack = false;
    public bool isBlocking = false;

    void Start()
    {
        manager = GameManager.instance;
        player = manager.GetPlayer();
        if(player == null)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnMove(InputAction.CallbackContext context) // WSAD or Left Stick
    {
        Vector2 v = context.ReadValue<Vector2>();
        player.SetMove(v);
    }

    public void OnJump(InputAction.CallbackContext context) // Spacekey or South Button
    {
        inputJump = context.performed;
        player.SetJump(inputJump);
    }

    public void OnPunch(InputAction.CallbackContext context) // U or West Button
    {

        inputPunch = context.performed;
        player.SetPunch(inputPunch);
    }

    public void OnKick(InputAction.CallbackContext context) // I or North Button
    {
        inputKick = context.performed;
        player.SetKick(inputKick);
    }

    public void OnSpecialAttack(InputAction.CallbackContext context) // O or Left Shoulder
    {
        inputSpecialAttack = context.performed;
        player.SetSpecialAttack(inputSpecialAttack);
    }

    public void OnBlocking(InputAction.CallbackContext context) // K or Right Shoulder
    {
        isBlocking = context.performed;
        player.SetBlocking(isBlocking);
    }





}
