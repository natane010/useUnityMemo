using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : CameraBase
{
    protected override void ExcuteCaneraStart()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(targetObject.transform.position, this.transform.position);
    }
    protected override void ExcuteCameraUpdate()
    {
        //float inputValue = Input.GetAxis("Horizontal");
        float inputValue = 0.0f;
        if (Input.GetKey(KeyCode.E))
        {

        }
        else if (Input.GetKey(KeyCode.Q))
        {

        }
        this.transform.RotateAround(targetObject.transform.position, Vector3.up, inputValue);
    }
}
