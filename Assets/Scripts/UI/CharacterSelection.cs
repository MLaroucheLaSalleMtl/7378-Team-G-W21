using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public Vector3 playerSpawnPosition = new Vector3(2.56f, 0.07f, -5.76f);
    public GameObject playerTest;

    public void OnCharacterSelection(int charSelected)
    {
        playerTest = characters[charSelected];
        GameObject player = Instantiate(characters[charSelected], playerSpawnPosition, Quaternion.identity);
        DontDestroyOnLoad(player);
        LoadScene.instance.LoadNextLevel();
    }
}
