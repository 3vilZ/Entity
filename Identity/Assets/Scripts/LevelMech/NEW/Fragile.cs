using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragile : MonoBehaviour
{
    [HideInInspector] public bool bKeepInfo = false;

    public void Reset()
    {
        if(!bKeepInfo)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;

            transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
            transform.GetChild(1).GetComponent<Collider2D>().enabled = true;
        }
    }
    public void Crash()
    {
        GameManager.Instance.CheckIfLobby(gameObject);
        GameManager.Instance.KeepInfo(this);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        transform.GetChild(1).GetComponent<Collider2D>().enabled = false;
    }
}
