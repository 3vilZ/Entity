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

    public void StartJump()
    {
        anim.SetTrigger("StartJump");
    }

    public void SwitchJump(bool b)
    {
        if(b)
        {
            anim.SetTrigger("SwitchJump");
        }
        else
        {
            anim.ResetTrigger("SwitchJump");
        }
    }

    public void Land(bool b)
    {
        if (b)
        {
            anim.SetTrigger("Land");
        }
        else
        {
            anim.ResetTrigger("Land");
        }
    }

    public void StartWall()
    {
        anim.SetTrigger("StartWall");
    }
    public void EndWall(bool b)
    {
        if (b)
        {
            anim.SetTrigger("EndWall");
        }
        else
        {
            anim.ResetTrigger("EndWall");
        }
    }

    public void Wall(bool b)
    {
        anim.SetBool("Wall", b);
    }
}
