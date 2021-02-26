using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDraw : MonoBehaviour
{
    Camera cam;
    Vector3 leftRay = new Vector3(600, 1000, 0);
    Vector3 rightRay = new Vector3(2400, 1000, 0);

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(leftRay);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;

            if (selection.CompareTag("Wall"))
            {
                Debug.Log("wall");
            }
        }

        Ray ray1 = cam.ScreenPointToRay(rightRay);
        Debug.DrawRay(ray1.origin, ray1.direction * 10, Color.yellow);
    }
}
