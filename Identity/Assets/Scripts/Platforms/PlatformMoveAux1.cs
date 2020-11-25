using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveAux1 : MonoBehaviour
{
    [SerializeField] GameObject goParent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //other.transform.parent = goParent.transform;
            other.GetComponent<PlayerControllerV3>().bPlatformMove = true;
            other.GetComponent<PlayerControllerV3>().goPlatformMove = goParent;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //other.transform.parent = null;
            other.GetComponent<PlayerControllerV3>().bPlatformMove = false;
            other.GetComponent<PlayerControllerV3>().goPlatformMove = goParent;
        }
    }
}
