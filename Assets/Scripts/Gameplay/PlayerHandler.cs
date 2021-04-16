using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Eduardo Worked on this Script 

public class PlayerHandler : MonoBehaviour
{
    GameManager manager;
    TutorialManager tutorialManager;
    TrainingManager trainingManager;
    private MyCharacterController player = null;
    private PauseMenu pauseMenu = null;

    private bool inputPunch = false;
    private bool inputKick = false;
    private bool inputSpecialAttack = false;
    public bool isBlocking = false;

    public bool isPaused = false;

    void Start()
    {
        pauseMenu = PauseMenu.instance;

        if(GameObject.Find("GameManager") != null)
        {
            manager = GameManager.instance;
            player = manager.GetPlayer();
            if (player == null)
            {
                Destroy(this.gameObject);
            }
        }

        else if (GameObject.Find("TutorialManager"))
        {
            tutorialManager = TutorialManager.instance;
            player = tutorialManager.GetPlayer();
            if (player == null)
            {
                Destroy(this.gameObject);
            }
        }

        else if (GameObject.Find("TrainingManager"))
        {
            trainingManager = TrainingManager.instance;
            player = trainingManager.GetPlayer();
            if (player == null)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context) // WSAD or Left Stick
    {
        Vector2 v = context.ReadValue<Vector2>();
        player.SetMove(v);
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
        if (player.GetComponent<FighterStatus>().HasSpecial())
        {
            inputSpecialAttack = context.performed;
            player.SetSpecialAttack(inputSpecialAttack);
        }
    }

    public void OnBlocking(InputAction.CallbackContext context) // K or Right Shoulder
    {
        isBlocking = context.performed;
        player.SetBlocking(isBlocking);
    }

    public void OnPause(InputAction.CallbackContext context) // ESC or Start
    {
        isPaused = context.performed;
        pauseMenu.Pause(isPaused);
    }
}
