using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rbBall;
    Vector3 v3BallSpeed;
    [SerializeField] int iDamage;

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
}
