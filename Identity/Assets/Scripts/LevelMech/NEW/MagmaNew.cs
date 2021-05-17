using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaNew : MonoBehaviour
{
    [SerializeField] public GameObject goGround;
    [SerializeField] public MeshRenderer meshRend;
    [SerializeField] Material matMagma;
    [SerializeField] Material matGround;

    [HideInInspector] public bool bKeepInfo = false;

    private void Awake()
    {
        goGround.transform.localScale = transform.localScale;
        goGround.GetComponent<BoxCollider2D>().size = new Vector2(1 / transform.localScale.x, 1 / transform.localScale.y);
    }
    private void Start()
    {
        goGround.SetActive(false);
    }

    public void Crash()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform current = transform.parent.GetChild(i);
            GameManager.Instance.KeepInfo(current.GetComponent<MagmaNew>());
            current.GetComponent<MagmaNew>().meshRend.material = matGround;
            current.GetComponent<Collider2D>().enabled = false;
            current.GetComponent<MagmaNew>().goGround.SetActive(true);

        }

        /*
        GameManager.Instance.KeepInfo(this);
        meshRend.material = matGround;
        GetComponent<Collider2D>().enabled = false;
        goGround.SetActive(true);
        */
    }

    public void Reset()
    {
        if (!bKeepInfo)
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                Transform current = transform.parent.GetChild(i);

                current.GetComponent<MagmaNew>().goGround.SetActive(false);
                current.GetComponent<MagmaNew>().meshRend.material = matMagma;
                current.GetComponent<Collider2D>().enabled = true;
                

            }

            /*
            goGround.SetActive(false);
            meshRend.material = matMagma;
            GetComponent<Collider2D>().enabled = true;
            */
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
            if (GameManager.Instance.GoBall.GetComponent<Ball>().currentPower == 3 && goGround.activeSelf == false)
            {
                GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
}
