using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour
{
    protected static GameObject targetObject;
    protected static float distance;

    void Start()
    {
        ExcuteCaneraStart();
    }
    void Update()
    {
        ExcuteCameraUpdate();
    }
    protected virtual void ExcuteCaneraStart() { }
    protected virtual void ExcuteCameraUpdate() { }
}
