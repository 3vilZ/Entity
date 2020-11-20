using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    [SerializeField] Collider2D colTop;
    [SerializeField] GameObject goBot;
    [SerializeField] float fCrashTime;
    [SerializeField] LayerMask layerGround;
    Rigidbody2D rb;
    Animator animator;
    bool bStart = false;
    float fCrashTimeControl = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        goBot.SetActive(false);

        fCrashTimeControl = fCrashTime;
    }

    
    void Update()
    {
        if(bStart)
        {
            fCrashTimeControl -= Time.deltaTime;

            if(fCrashTimeControl <= 0)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                animator.SetTrigger("Fall");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetTrigger("Crash");
            if(!bStart)
            {
                bStart = true;
            }
            
        }
        else
        {
            if(bStart)
            {
                bStart = false;
                rb.bodyType = RigidbodyType2D.Kinematic;
                animator.SetTrigger("Land");

                colTop.enabled = false;
                GetComponent<WallJump>().colLeft.enabled = false;
                GetComponent<WallJump>().colRight.enabled = false;
                goBot.SetActive(true);
            }
        }
    }
}
