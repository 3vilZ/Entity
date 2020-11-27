using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinerTransition : MonoBehaviour
{
    [SerializeField] GameObject goCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            goCamera.SetActive(true);
            GameManager.Instance.GoCurrentVirtualCamera = goCamera;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(GameObject.FindGameObjectsWithTag("VirtualCamera").Length > 1)
            {
                GameManager.Instance.GoOldVirtualCamera.SetActive(false);
                GameManager.Instance.GoOldVirtualCamera = goCamera;
            }
            
            
        }
    }



    /*
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
    */
}
