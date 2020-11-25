using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCondition : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().bCollecting = false;
            GameManager.Instance.Death1();
        }
    }
}
