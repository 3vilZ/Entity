using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Second : MonoBehaviour
{
    [SerializeField] public float fSectionTime;
    [SerializeField] GameObject goTrue;
    [SerializeField] GameObject goFalse;

    [HideInInspector] public float timerControl = 0;
    bool bPhysical = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!bPhysical)
        {
            goTrue.SetActive(false);
            goFalse.SetActive(true);
        }
        else
        {
            goTrue.SetActive(true);
            goFalse.SetActive(false);
        }

        if(timerControl > 0)
        {
            timerControl -= Time.deltaTime;
            bPhysical = true;
        }
        else
        {
            bPhysical = false;
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ball")
        {
            if(timerControl > fSectionTime)
            {
                timerControl = fSectionTime * 3;
            }
            else if(timerControl <= fSectionTime && timerControl > 0)
            {
                timerControl = fSectionTime * 2;
            }
            else
            {
                timerControl = fSectionTime;
            }
        }
    }
    */
}
