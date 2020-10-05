using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJoyStick : MonoBehaviour
{
    public float sensibility;

    private void Update()
    {
        float xInput = Input.GetAxis("CameraX");
        float yInput = Input.GetAxis("CameraY");

        transform.position += new Vector3(yInput, 0, 0) * sensibility;
    }
}
