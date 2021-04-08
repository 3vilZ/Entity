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

    Vector3 v3Initial;
    [HideInInspector] public bool bKeepInfo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        goBot.SetActive(false);

        fCrashTimeControl = fCrashTime;

        v3Initial = transform.position;
    }

    
    void Update()
    {
        if(bStart)
        {
            fCrashTimeControl -= Time.deltaTime;

            if(fCrashTimeControl <= 0)
            {
                colTop.isTrigger = true;
                rb.bodyType = RigidbodyType2D.Dynamic;
                animator.SetTrigger("Fall");
            }
        }
    }

    public void Crash()
    {
        if (!bStart)
        {
            animator.SetTrigger("Crash");
            GameManager.Instance.KeepInfo(this);
            bStart = true;
        }
    }

    public void Reset()
    {
        if(!bKeepInfo)
        {
            print("olis");
            bStart = false;
            animator.ResetTrigger("Crash");
            animator.ResetTrigger("Fall");
            animator.ResetTrigger("Land");
            animator.SetTrigger("Reset");
            fCrashTimeControl = fCrashTime;
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.position = v3Initial;
            colTop.enabled = true;
            colTop.isTrigger = false;
            GetComponent<WallJump>().colLeft.enabled = true;
            GetComponent<WallJump>().colRight.enabled = true;

            goBot.SetActive(false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (bStart && other.gameObject.tag != "Player" && other.gameObject.tag != "Ball")
        {
            
            rb.bodyType = RigidbodyType2D.Static;
            transform.position = goBot.transform.position + new Vector3(0, 1, 0);
            animator.SetTrigger("Land");

            colTop.enabled = false;
            GetComponent<WallJump>().colLeft.enabled = false;
            GetComponent<WallJump>().colRight.enabled = false;

            goBot.SetActive(true);
            bStart = false;
        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ball")
        {
            animator.SetTrigger("Crash");
            if(!bStart)
            {
                bStart = true;
            }
            
        }
    }
*/
}
