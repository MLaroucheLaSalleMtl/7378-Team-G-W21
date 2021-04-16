using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Eduardo Worked on this Script 

public class InvisibleWalls : MonoBehaviour
{
    Camera cam;
    public Transform leftWall;
    public Transform rightWall;
    private Vector3 wallLPos;
    private Vector3 wallRPos;
    public LayerMask wallPlaneLayer;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Ray rayLeft = cam.ScreenPointToRay(new Vector3(0, 0));
        Ray rayRight = cam.ScreenPointToRay(new Vector3(Screen.width,0));
        Debug.DrawRay(rayLeft.origin, rayLeft.direction * 10, Color.yellow);
        Debug.DrawRay(rayRight.origin, rayRight.direction * 10, Color.red);
        RaycastHit hit;
       
        //Left Wall
        if(Physics.Raycast(rayLeft, out hit, 100, wallPlaneLayer))
        {
            wallLPos = hit.point;
        }
        wallLPos.y = leftWall.position.y;
        wallLPos.z = leftWall.position.z;
        leftWall.position = wallLPos;

        //Right Wall
        if (Physics.Raycast(rayRight, out hit, 100, wallPlaneLayer))
        {
            wallRPos = hit.point;
        }
        wallRPos.y = rightWall.position.y;
        wallRPos.z = rightWall.position.z;
        rightWall.position = wallRPos;
        
    }
}
