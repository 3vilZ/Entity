using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFalse : MonoBehaviour
{
    public Second second;

    void Update()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.Instance.GoPlayer.GetComponent<Collider2D>());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ball")
        {
            second.timerControl = second.fSectionTime;
            AudioManager.Instance.PlayMechFx("Second1");
        }
    }
}
