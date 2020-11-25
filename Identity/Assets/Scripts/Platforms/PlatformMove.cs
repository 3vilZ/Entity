using System.Collections;
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

    LineRenderer lineRenderer;

    float fPlayerSpeed;

    void Start()
    {
        iCurrentPoint = 1;
        fPlayerSpeed = fForwardSpeed;

        lineRenderer = this.GetComponent<LineRenderer>();

        lineRenderer.positionCount = tPoints.Length;

        for (int i = 0; i < tPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, tPoints[i].transform.position);
        }
    }

    
    void FixedUpdate()
    {
        if (bMoving)
        {
            if (!bBackward)
            {
                v3CurrentVector = tPoints[iCurrentPoint].position - transform.position;
                v3CurrentVector.Normalize();

                //transform.position += v3CurrentVector * fForwardSpeed * Time.deltaTime;
                //transform.Translate(v3CurrentVector * fForwardSpeed * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, tPoints[iCurrentPoint].position, fForwardSpeed * Time.deltaTime);
                GetComponent<Rigidbody2D>().velocity = v3CurrentVector * fForwardSpeed;

                if (Vector3.Distance(transform.position, tPoints[iCurrentPoint].position) <= 0.2f)
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

                //transform.position += v3CurrentVector * fForwardSpeed * Time.deltaTime;
                //transform.Translate(v3CurrentVector * fForwardSpeed * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, tPoints[iCurrentPoint].position, fForwardSpeed * Time.deltaTime);
                GetComponent<Rigidbody2D>().velocity = v3CurrentVector * fForwardSpeed;

                if (Vector3.Distance(transform.position, tPoints[iCurrentPoint].position) <= 0.2f)
                {
                    if (iCurrentPoint == 0)
                    {
                        iCurrentPoint++;
                        bMoving = false;
                        bBackward = false;
                        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
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
