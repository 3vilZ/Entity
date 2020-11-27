using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinerTransition : MonoBehaviour
{
    [SerializeField] GameObject goVirtualCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !other.isTrigger)
        {
            goVirtualCamera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !other.isTrigger)
        {
            goVirtualCamera.SetActive(false);
        }
    }
}
