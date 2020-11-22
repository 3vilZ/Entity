using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] Color colorBase;
    [SerializeField] Color colorCollecting;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = colorBase;
    }

    private void Update()
    {
        if(GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().bCollecting == false)
        {
            GetComponent<SpriteRenderer>().color = colorBase;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Ball")
        {
            GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().SetCollectable(gameObject);
            GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().bCollecting = true;
            GetComponent<SpriteRenderer>().color = colorCollecting;
        }
    }
}
