using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] float fPlayerSpeed;
    [SerializeField] float fBallSpeed;
    [SerializeField] bool bInverse = false;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if(bInverse)
        {
            fPlayerSpeed = -fPlayerSpeed;
            fBallSpeed = -fBallSpeed;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetTrigger("Push");
            GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity.x, fPlayerSpeed);
            AudioManager.Instance.PlaySound("Jump");
            AudioManager.Instance.PlayMechFx("Trampoline");
        }
        if (other.gameObject.tag == "Ball")
        {
            animator.SetTrigger("Push");
            if (GameManager.Instance.GoBall.GetComponent<Ball>().currentPower == 3)
            {
                GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity = new Vector2(GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity.x, 10f);
            }
            else
            {
                GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity = new Vector2(GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity.x, fBallSpeed);
            }
            AudioManager.Instance.PlaySound("Shoot");
            AudioManager.Instance.PlayMechFx("Trampoline");
        }
    }
}
