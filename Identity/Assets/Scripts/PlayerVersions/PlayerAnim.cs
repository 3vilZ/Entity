using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator anim;
    bool bWalking = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartWalk()
    {
        if(!bWalking)
        {
            anim.SetTrigger("StartWalk");
        }
    }

    public void Walking(bool b)
    {
        bWalking = b;
        anim.SetBool("Walking", bWalking);
    }
}
