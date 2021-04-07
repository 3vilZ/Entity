using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("New")]
    [SerializeField] GameObject[] goPowers;
    
    [HideInInspector] public int currentPower;
    [HideInInspector] public Vector2 v2DirectionIron;


    [Header("Old")]
    [SerializeField] int iDamage;
    [SerializeField] ParticleSystem BallPS;
    [Space(10)]
    public GameObject goLimit;
    public GameObject goBallArrow;
    public Transform tBallAttackPivot;

    Rigidbody2D rbBall;
    Vector3 v3BallSpeed;
    Quaternion targetRot;
    List<ParticleSystem> psList = new List<ParticleSystem>();
    bool bEmitting;

    private void Start()
    {
        rbBall = GetComponent<Rigidbody2D>();

        for (int i = 0; i < goPowers.Length; i++)
        {
            goPowers[i].SetActive(false);
        }
    }

    public void GetPowerID(int ID)
    {
        currentPower = ID;

        if(currentPower == 2)
        {
            GameManager.Instance.ScriptPlayer.BubbleShootForce(true);
        }
        else
        {
            GameManager.Instance.ScriptPlayer.BubbleShootForce(false);
        }

        if(currentPower == 3)
        {
            GameManager.Instance.ScriptPlayer.IronState(true);
        }
        else
        {
            GameManager.Instance.ScriptPlayer.IronState(false);
        }

        for (int i = 0; i < goPowers.Length; i++)
        {
            goPowers[i].SetActive(false);
        }

        goPowers[currentPower].SetActive(true);
    }

    public void RemovePower()
    {
        currentPower = -1;

        GameManager.Instance.ScriptPlayer.BubbleShootForce(false);
        GameManager.Instance.ScriptPlayer.IronState(false);

        for (int i = 0; i < goPowers.Length; i++)
        {
            goPowers[i].SetActive(false);
        }
    }
    
    private void Update()
    {
        if (psList.Count != 0)
        {
            if (psList[0])
            {
                if (!psList[0].IsAlive())
                {
                    Destroy(psList[0].gameObject);
                    psList.Remove(psList[0]);
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentPower == 2)
        {
            if (other.gameObject.tag == "Fire")
            {
                GameManager.Instance.ScriptPlayer.BubbleForceReload();
                other.gameObject.GetComponent<Fire>().Crash();
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(currentPower == 0)
        {
            if(other.gameObject.tag == "Fragile")
            {
                other.gameObject.GetComponent<Fragile>().Crash();
            }
            else if(other.gameObject.tag == "Fall")
            {
                other.gameObject.GetComponent<PlatformFall>().Crash();
            }
            else if (other.gameObject.tag == "Arrow")
            {
                other.gameObject.GetComponent<Projectile>().Crash();
            }
            else if (other.gameObject.tag == "Laser")
            {
                other.gameObject.GetComponent<Laser>().Crash();
            }


        }
        else if(currentPower == 1)
        {

        }
        else if (currentPower == 2)
        {
            if(other.gameObject.tag == "Spikes" || other.gameObject.tag == "Bullet")
            {
                GameManager.Instance.ScriptPlayer.BubbleForceReload();

            }
            else if (other.gameObject.tag == "Magma")
            {
                GameManager.Instance.ScriptPlayer.BubbleForceReload();
                other.gameObject.GetComponent<Magma>().Crash();
            }
            //Fire and Laser on his own code
        }

        //Iron + Spikes/Magma on his own code


        if (other.GetContact(0).normal.x > 0.5f || other.GetContact(0).normal.x < -0.5f)
        {
            targetRot = Quaternion.LookRotation(other.GetContact(0).normal, Vector3.forward);
        }
        else
        {
            targetRot = Quaternion.LookRotation(other.GetContact(0).normal, Vector3.up);
        }

        psList.Add(Instantiate(BallPS, other.GetContact(0).point, targetRot));

        for (int i = 0; i < psList.Count; i++)
        {
            if(!psList[i].IsAlive())
            {
                psList[i].Play();
            }
        }



        //ParticleSystem j = Instantiate(BallPS, collision.GetContact(0).point, targetRot);
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10);
    }
}
