using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] float fPlayerSpeed;
    [SerializeField] float fBallSpeed;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity.x, fPlayerSpeed);
        }
        if (other.gameObject.tag == "Ball")
        {
            if(GameManager.Instance.GoBall.GetComponent<Ball>().currentPower == 3)
            {
                GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity = new Vector2(GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity.x, 10f);
            }
            else
            {
                GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity = new Vector2(GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity.x, fBallSpeed);
            }
        }
    }
}
