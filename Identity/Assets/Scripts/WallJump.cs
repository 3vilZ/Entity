using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    [SerializeField] bool bAutoWall;

    GameObject goRight;
    GameObject goLeft;

    public BoxCollider2D colRight;
    public BoxCollider2D colLeft;

    void Start()
    {
        if(bAutoWall)
        {
            goRight = new GameObject("Right");
            goLeft = new GameObject("Left");

            goRight.transform.parent = transform;
            goLeft.transform.parent = transform;

            goRight.transform.position = new Vector2(transform.position.x + (transform.localScale.x * 0.5f), transform.position.y);
            goLeft.transform.position = new Vector2(transform.position.x - (transform.localScale.x * 0.5f), transform.position.y);

            goRight.layer = LayerMask.NameToLayer("Wall");
            goLeft.layer = LayerMask.NameToLayer("Wall");

            colRight = goRight.AddComponent<BoxCollider2D>();
            colLeft = goLeft.AddComponent<BoxCollider2D>();

            colRight.isTrigger = true;
            colLeft.isTrigger = true;

            colRight.offset = new Vector2(-0.05f, 0);
            colLeft.offset = new Vector2(0.05f, 0);

            colRight.size = new Vector2(0.1f, transform.localScale.y);
            colLeft.size = new Vector2(0.1f, transform.localScale.y);
        }
    }
}
