﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] Transform[] tPoints;
    [SerializeField] float fForwardSpeed;
    [SerializeField] float fAcceleration;
    [SerializeField] float fBackSpeed;
    Vector3 v3CurrentVector;
    int iCurrentPoint;
    bool bMoving = false;
    bool bPlaced = false;
    bool bBackward = false;

    float fPlayerSpeed;

    void Start()
    {
        iCurrentPoint = 1;
        fPlayerSpeed = fForwardSpeed;
    }

    
    void FixedUpdate()
    {
        if(bPlaced)
        {
            GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity += new Vector2(v3CurrentVector.x, v3CurrentVector.y) * fForwardSpeed;
        }

        if (bMoving)
        {
            if (!bBackward)
            {
                transform.position = Vector3.MoveTowards(transform.position, tPoints[iCurrentPoint].position, fForwardSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, tPoints[iCurrentPoint].position) <= 0.1f)
                {
                    if (iCurrentPoint == tPoints.Length - 1)
                    {
                        iCurrentPoint--;
                        bBackward = true;
                    }
                    else
                    {
                        iCurrentPoint++;
                    }
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, tPoints[iCurrentPoint].position, fForwardSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, tPoints[iCurrentPoint].position) <= 0.1f)
                {
                    if (iCurrentPoint == 0)
                    {
                        iCurrentPoint++;
                        bMoving = false;
                        bBackward = false;
                    }
                    else
                    {
                        iCurrentPoint--;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && !bMoving || other.gameObject.tag == "Ball" && !bMoving)
        {
            bMoving = true;
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !bMoving)
        {
            bMoving = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = transform;
            //bPlaced = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = null;
            //bPlaced = false;
        }
    }
}

/*
 * if (bMoving)
        {
            if (!bBackward)
            {
                v3CurrentVector = tPoints[iCurrentPoint].position - transform.position;
                v3CurrentVector.Normalize();

                GetComponent<Rigidbody2D>().velocity = v3CurrentVector * fForwardSpeed;
                

                if (Vector3.Distance(transform.position, tPoints[iCurrentPoint].position) <= 0.5f)
                {
                    if (iCurrentPoint == tPoints.Length - 1)
                    {
                        iCurrentPoint--;
                        bBackward = true;
                    }
                    else
                    {
                        iCurrentPoint++;
                    }
                }
            }
            else
            {
                v3CurrentVector = tPoints[iCurrentPoint].position - transform.position;
                v3CurrentVector.Normalize();

                GetComponent<Rigidbody2D>().velocity = v3CurrentVector * fForwardSpeed;
                
                

                if (Vector3.Distance(transform.position, tPoints[iCurrentPoint].position) <= 0.5f)
                {
                    if (iCurrentPoint == 0)
                    {
                        iCurrentPoint++;
                        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                        bMoving = false;
                        bBackward = false;
                    }
                    else
                    {
                        iCurrentPoint--;
                    }
                }
            }
        }
 * 
 * 
   if(bMoving)
        {
            if (!bBackward)
            {
                transform.position = Vector3.MoveTowards(transform.position, tPoints[iCurrentPoint].position, fForwardSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, tPoints[iCurrentPoint].position) <= 0.1f)
                {
                    if (iCurrentPoint == tPoints.Length - 1)
                    {
                        iCurrentPoint--;
                        bBackward = true;
                    }
                    else
                    {
                        iCurrentPoint++;
                    }
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, tPoints[iCurrentPoint].position, fForwardSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, tPoints[iCurrentPoint].position) <= 0.1f)
                {
                    if (iCurrentPoint == 0)
                    {
                        iCurrentPoint++;
                        bMoving = false;
                        bBackward = false;
                    }
                    else
                    {
                        iCurrentPoint--;
                    }
                }
            }
        }
 */
