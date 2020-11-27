using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ConfinerTransition : MonoBehaviour
{
    [SerializeField] GameObject goCamera;
    [SerializeField] bool bStartingCamera = false;

    private void Start()
    {
        goCamera.GetComponent<CinemachineVirtualCamera>().m_Follow = GameManager.Instance.GoPlayer.transform;

        if(bStartingCamera)
        {
            GameManager.Instance.GoCurrentVirtualCamera = goCamera;
            GameManager.Instance.GoOldVirtualCamera = goCamera;
            GameManager.Instance.GoCheckPointCamera = goCamera;
        }
        else
        {
            goCamera.SetActive(false);
        }
    }

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
