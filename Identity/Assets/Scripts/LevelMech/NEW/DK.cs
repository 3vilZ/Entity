﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DK : MonoBehaviour
{
    [SerializeField] float fPlayerSpeed;
    [SerializeField] float fBallSpeed;
    [SerializeField] float fLoadTime;
    [SerializeField] float fCapMoveTime;
    [SerializeField] Transform tDirection;


    float fPlayerLoadControl;
    float fBallLoadControl;
    float fCapMoveControl;

    Vector2 v2direction;

    bool bPlayerIn = false;
    bool bBallIn = false;

    private void Start()
    {
        fPlayerLoadControl = fLoadTime;
        fBallLoadControl = fLoadTime;
        fCapMoveControl = fCapMoveTime;
        v2direction = tDirection.position - transform.position;
        v2direction.Normalize();
    }

    private void Update()
    {
        if(bPlayerIn)
        {
            GameManager.Instance.ScriptPlayer.DKReload();
            GameManager.Instance.ScriptPlayer.tModel.gameObject.SetActive(false);
            fPlayerLoadControl -= Time.deltaTime;

            if(fPlayerLoadControl <= 0)
            {
                Launch();
            }
        }
        if (bBallIn)
        {
            fBallLoadControl -= Time.deltaTime;

            if (fBallLoadControl <= 0)
            {
                Launch();
            }
        }

        /*
        if(GameManager.Instance.ScriptPlayer.bDK == true)
        {
            fCapMoveControl -= Time.deltaTime;
            if(fCapMoveControl <= 0)
            {
                fCapMoveControl = fCapMoveTime;
                GameManager.Instance.ScriptPlayer.bDK = false;
            }
        }
        */
    }

    void Launch()
    {
        if (bPlayerIn)
        {
            GameManager.Instance.LookAheadSmoothing(false);

            GameManager.Instance.ScriptPlayer.tModel.gameObject.SetActive(true);
            GameManager.Instance.ScriptPlayer.bDK = true;
            GameManager.Instance.ScriptPlayer.bInteracting = false;
            GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity = v2direction * fPlayerSpeed;
            fPlayerLoadControl = fLoadTime;
            bPlayerIn = false;    
        }
        if (bBallIn)
        {
            if(GameManager.Instance.GoBall.GetComponent<Ball>().currentPower == 3)
            {
                GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity = v2direction * 10f;
            }
            else if(GameManager.Instance.GoBall.GetComponent<Ball>().currentPower == 2)
            {
                GameManager.Instance.ScriptPlayer.BubbleShootForce(true);
                GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity = v2direction * fBallSpeed;
            }
            else
            {
                GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity = v2direction * fBallSpeed;
            }
            
            fBallLoadControl = fLoadTime;
            bBallIn = false;
        }

        AudioManager.Instance.PlayMechFx("DKLaunch");
    }
    void LaunchBall()
    {


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.LookAheadSmoothing(true);

            GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GameManager.Instance.GoPlayer.transform.position = transform.position;
            GameManager.Instance.ScriptPlayer.bInteracting = true;
            GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            fBallLoadControl = fLoadTime;

            //GameManager.Instance.LookAheadSmoothing(false);
            AudioManager.Instance.PlayMechFx("DKIn");
            bPlayerIn = true;
            
        }
        if (other.gameObject.tag == "Ball" && !GameManager.Instance.GoBall.GetComponent<CircleCollider2D>().isTrigger)
        {
            if(GameManager.Instance.GoBall.GetComponent<Ball>().currentPower == 2)
            {
                //other.GetComponent<Rigidbody2D>().gravityScale = 0;
                GameManager.Instance.ScriptPlayer.BubbleShootForce(false);
            }
            GameManager.Instance.GoBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GameManager.Instance.GoBall.transform.position = transform.position;
            AudioManager.Instance.PlayMechFx("DKIn");
            //CapShootReload
            bBallIn = true;
            
        }
    }
}
