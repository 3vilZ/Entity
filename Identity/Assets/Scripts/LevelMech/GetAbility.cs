﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAbility : MonoBehaviour
{
    [SerializeField] int iValue;
    [SerializeField] float fDistance;
    [SerializeField] GameObject goY;
    [SerializeField] GameObject goBallSlot;
    [SerializeField] GameObject goBallHalo;
    [SerializeField] GameObject goAnimPlace;
    [SerializeField] GameObject[] goParts;
    [SerializeField] ParticleSystem[] ps;

    Animator animAbility;
    bool bOnce = false;
    bool bStart = false;
    bool bEnd = false;

    private void Start()
    {
        goY.SetActive(false);
        animAbility = GetComponent<Animator>();

        for (int i = 0; i < goParts.Length; i++)
        {
            goParts[i].SetActive(false);
        }

        goParts[iValue].SetActive(true);

        if(!GameManager.Instance.BSkill[0])
        {
            GameManager.Instance.GoBall.transform.position = goBallSlot.transform.position;
            GameManager.Instance.GoBall.transform.parent = goBallSlot.transform;
            goBallHalo.SetActive(true);
        }
        else
        {
            goParts[0].SetActive(true);
            goBallHalo.SetActive(false);
        }
    }

    private void Update()
    {
        if(!bOnce)
        {
            if (Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position) <= fDistance)
            {
                goY.SetActive(true);

                if (Input.GetButtonDown("Y"))
                {
                    GameManager.Instance.ScriptPlayer.bInteracting = true;
                    GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity.y);

                    if (GameManager.Instance.BSkill[0])
                    {
                        GameManager.Instance.ScriptPlayer.CatchBall();
                        goBallSlot.transform.position = GameManager.Instance.GoBall.transform.position;
                        GameManager.Instance.GoBall.transform.parent = goBallSlot.transform;
                    }

                    bStart = true;
                    goY.SetActive(false);
                    bOnce = true;
                }
            }
            else
            {
                goY.SetActive(false);
            }
        }
        

        if(bStart)
        {
            goBallSlot.transform.position = Vector3.MoveTowards(goBallSlot.transform.position, goAnimPlace.transform.position, 0.03f);

            if(Vector3.Distance(goBallSlot.transform.position, goAnimPlace.transform.position) <= 0.1f)
            {
                animAbility.SetTrigger("Start");
                bStart = false;
            }
        }

        if(bEnd)
        {
            goBallSlot.transform.position = Vector3.MoveTowards(goBallSlot.transform.position, GameManager.Instance.GoPlayer.transform.position, 0.03f);

            if (Vector3.Distance(goBallSlot.transform.position, GameManager.Instance.GoPlayer.transform.position) <= 0.1f)
            {
                GameManager.Instance.ScriptPlayer.CatchBall();
                GameManager.Instance.ScriptPlayer.bInteracting = false;
                GameManager.Instance.GetSkill(iValue);
                bEnd = false;
            }
        }
    }

    public void Particles()
    {
        for (int i = 0; i < ps.Length; i++)
        {
            ps[i].Play();
        }

        /*
        if (!ps[7].IsAlive())
        {
            GameManager.Instance.ScriptPlayer.bAbility = false;
            bStart = false;
        }
        */
    }

    public void EndAnim()
    {
        bEnd = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fDistance);
    }
}
