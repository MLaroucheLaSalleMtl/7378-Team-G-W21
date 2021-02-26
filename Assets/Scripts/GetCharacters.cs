using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCharacters : MonoBehaviour
{

    public GameObject[] Characters;

    public GameObject Place;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Characters") == 0)
        {
            GameObject obj = Instantiate(Characters[0] , Place.transform.position , Quaternion.identity);
        } 
        else if (PlayerPrefs.GetInt("Characters") == 1)
        {
            GameObject obj = Instantiate(Characters[1], Place.transform.position, Quaternion.identity);
        }
        else if (PlayerPrefs.GetInt("Characters") == 2)
        {
            GameObject obj = Instantiate(Characters[2], Place.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
