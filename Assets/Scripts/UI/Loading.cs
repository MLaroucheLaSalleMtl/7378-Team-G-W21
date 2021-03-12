using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Loading : MonoBehaviour
{
    private AsyncOperation async;
    [SerializeField] private Image filledImage;
    [SerializeField] private Text txtPercent;

    [SerializeField] private bool waitForUserInput = false;
    private bool ready = false;
    private bool anyKey = false;


    public void OnAnyKey(InputAction.CallbackContext context)
    {

        anyKey = true;
    }




    void Start()
    {
        Time.timeScale = 1.0f;
        Input.ResetInputAxes();
        System.GC.Collect();
        //Scene currentScene = SceneManager.GetActiveScene();
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        if(!waitForUserInput)
        {
            ready = true;
        }


    }

  
    void Update()
    {
        if(waitForUserInput && anyKey)
        {
            if(async.progress >= 0.9f)
            {
                ready = true;
            }


        }
        if (filledImage)
        {
            filledImage.fillAmount = async.progress + 0.1f;
        }

        if (txtPercent)
        {
            txtPercent.text = ((async.progress + 0.1f) * 100) + " %";
        }



        if (async.progress >= 0.9f && ready)
        {
            async.allowSceneActivation = true;
        }
                    
    }
}
