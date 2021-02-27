using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
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
            GameManager.Instance.GoBall.GetComponent<Rigidbody2D>();
        }
    }
}
