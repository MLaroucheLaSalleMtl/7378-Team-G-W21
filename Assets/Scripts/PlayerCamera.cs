using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform[] playerTransforms;
    [SerializeField] private float yOffset = 2.0f;
    [SerializeField] private float minDistance = 7.5f;
    [SerializeField] private float maxDistance = 20.0f;

    [SerializeField] private float xMin, xMax, yMin, yMax;
    private float xMiddle;
    private float yMiddle;
    private float distance;

    void Start()
    {
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        playerTransforms = new Transform[playerList.Length];
        for(int i = 0; i < playerList.Length; i++)
        {
            playerTransforms[i] = playerList[i].transform;
        }
    }

    private void LateUpdate()
    {
        if(playerTransforms.Length == 0)
        {
            return;
        }

        xMin = xMax = playerTransforms[0].position.x;
        yMin = yMax = playerTransforms[0].position.y;

        for(int i = 1; i < playerTransforms.Length; i++)
        {
            if(playerTransforms[i].position.x < xMin)
            {
                xMin = playerTransforms[i].position.x;
            }

            if (playerTransforms[i].position.x > xMax)
            {
                xMax = playerTransforms[i].position.x;
            }

            if (playerTransforms[i].position.x < yMin)
            {
                yMin = playerTransforms[i].position.y;
            }

            if (playerTransforms[i].position.x > yMax)
            {
                yMax = playerTransforms[i].position.y;
            }
        }

        xMiddle = (xMin + xMax) / 2;
        yMiddle = (yMin + yMax) / 2;
        distance = xMax - xMin;

        if(distance < minDistance)
        {
            distance = minDistance;
        }
        /*
        if (distance > maxDistance)
        {
            distance = maxDistance;
        }
        */
        transform.position = new Vector3(xMiddle, yMiddle + yOffset, distance - 10);
    }
}
