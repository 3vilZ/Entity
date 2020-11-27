using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rbBall;
    Vector3 v3BallSpeed;
    [SerializeField] int iDamage;
    [SerializeField] ParticleSystem BallPS;
    [Space(10)]
    public GameObject goLimit;
    public GameObject goBallArrow;
    public Transform tBallAttackPivot;

    Quaternion targetRot;
    List<ParticleSystem> psList = new List<ParticleSystem>();
    bool bEmitting;

    private void Start()
    {
        rbBall = GetComponent<Rigidbody2D>();
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

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerControllerV2>().CatchBall();
        }
        else if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponentInParent<EnemyBase>().GetDamage(iDamage);
        }
    }
    */

    private void OnDrawGizmosSelected()
    {

        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.GetContact(0).normal.x > 0.5f || collision.GetContact(0).normal.x < -0.5f)
        {
            targetRot = Quaternion.LookRotation(collision.GetContact(0).normal, Vector3.forward);
        }
        else
        {
            targetRot = Quaternion.LookRotation(collision.GetContact(0).normal, Vector3.up);
        }

        psList.Add(Instantiate(BallPS, collision.GetContact(0).point, targetRot));

        for (int i = 0; i < psList.Count; i++)
        {
            if(!psList[i].IsAlive())
            {
                psList[i].Play();
            }
        }



        //ParticleSystem j = Instantiate(BallPS, collision.GetContact(0).point, targetRot);
    }

    
}
