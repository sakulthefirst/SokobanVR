using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDirectionResolver : MonoBehaviour
{


    // Set position to camera direction
    void Update()
    {
        var camera = Camera.main.transform;

        // Director vector
        Vector3 director = camera.forward;

        // Look at rotation
        Quaternion inverseRot = Quaternion.LookRotation(director);
        transform.rotation = inverseRot;

        // Position
        Vector3 newPos = camera.position + (director);
        transform.position = newPos;
    }
}
