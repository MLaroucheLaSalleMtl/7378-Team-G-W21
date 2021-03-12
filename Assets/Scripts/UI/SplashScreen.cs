using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]

    void Start()
    {
        StartCoroutine("CountDown");
    }

    public IEnumerator CountDown()
    {

        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(0);

        yield return new WaitForSeconds(3);
        Application.LoadLevel(1);
    }
}
