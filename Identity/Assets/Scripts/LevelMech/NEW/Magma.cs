using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    [SerializeField] GameObject goPlatform;

    [HideInInspector] public bool bKeepInfo = false;

    private void Start()
    {
        goPlatform.SetActive(false);
    }

    public void Crash()
    {
        GameManager.Instance.KeepInfo(this);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        goPlatform.SetActive(true);
    }

    public void Reset()
    {
        if (!bKeepInfo)
        {
            goPlatform.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }           
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().bCollecting = false;

            if (GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().goCurrentCollectable != null)
            {
                GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().goCurrentCollectable.GetComponent<Collectable>().bCollecting = false;
            }

            GameManager.Instance.ScriptPlayer.BoolDeathDone();
            GameManager.Instance.Death1();
        }

        if (other.gameObject.tag == "Ball")
        {
            if (GameManager.Instance.GoBall.GetComponent<Ball>().currentPower == 3 && goPlatform.activeSelf == false)
            {
                GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
}
