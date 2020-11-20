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


    private void Start()
    {
        rbBall = GetComponent<Rigidbody2D>();
    }

    
    private void Update()
    {
        /*
        v3BallSpeed = rbBall.velocity;
        v3BallSpeed.Normalize();
        print(v3BallSpeed);
        */
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
        //print("collision");
        Quaternion rotToLook = Quaternion.LookRotation(collision.contacts[0].normal);
        Vector3 m = rotToLook.eulerAngles;
        m.z = 90;

        rotToLook = Quaternion.Euler(m);
        
        ParticleSystem j = Instantiate(BallPS, collision.contacts[0].point, rotToLook);
        
    }

    
}
