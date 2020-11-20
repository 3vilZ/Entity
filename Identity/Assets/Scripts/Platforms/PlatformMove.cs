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
    bool bBackward = false;


    void Start()
    {
        iCurrentPoint = 1;
    }

    
    void Update()
    {
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && !bMoving)
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
