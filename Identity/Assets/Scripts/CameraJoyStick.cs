using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJoyStick : MonoBehaviour
{
    [SerializeField] Transform tPlayer;

    private void Update()
    {
        transform.position = new Vector3(tPlayer.position.x, transform.position.y, transform.position.z);
    }
}
